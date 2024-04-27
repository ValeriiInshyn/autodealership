using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using CourseWork.Data;
using CourseWork.Models.AutoDealership;

namespace CourseWork
{
    [SuppressMessage("ReSharper", "MethodHasAsyncOverload")]
    public partial class AutoDealershipService
    {
        AutoDealershipContext Context => this.context;

        private readonly AutoDealershipContext context;
        private readonly NavigationManager navigationManager;

        public AutoDealershipService(AutoDealershipContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAutoDealershipsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/autodealerships/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/autodealerships/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAutoDealershipsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/autodealerships/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/autodealerships/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAutoDealershipsRead(ref IQueryable<CourseWork.Models.AutoDealership.AutoDealership> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.AutoDealership>> GetAutoDealerships(Query query = null)
        {
            var items = Context.AutoDealerships.AsQueryable();

            items = items.Include(i => i.City);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAutoDealershipsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAutoDealershipGet(CourseWork.Models.AutoDealership.AutoDealership item);
        partial void OnGetAutoDealershipById(ref IQueryable<CourseWork.Models.AutoDealership.AutoDealership> items);


        public async Task<CourseWork.Models.AutoDealership.AutoDealership> GetAutoDealershipById(int id)
        {
            var items = Context.AutoDealerships
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.City);

            OnGetAutoDealershipById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAutoDealershipGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAutoDealershipCreated(CourseWork.Models.AutoDealership.AutoDealership item);
        partial void OnAfterAutoDealershipCreated(CourseWork.Models.AutoDealership.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealership.AutoDealership> CreateAutoDealership(CourseWork.Models.AutoDealership.AutoDealership autodealership)
        {
            OnAutoDealershipCreated(autodealership);

            var existingItem = Context.AutoDealerships
                .FirstOrDefault(i => i.Id == autodealership.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.AutoDealerships.Add(autodealership);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(autodealership).State = EntityState.Detached;
                throw;
            }

            OnAfterAutoDealershipCreated(autodealership);

            return autodealership;
        }

        public async Task<CourseWork.Models.AutoDealership.AutoDealership> CancelAutoDealershipChanges(CourseWork.Models.AutoDealership.AutoDealership item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAutoDealershipUpdated(CourseWork.Models.AutoDealership.AutoDealership item);
        partial void OnAfterAutoDealershipUpdated(CourseWork.Models.AutoDealership.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealership.AutoDealership> UpdateAutoDealership(int id, CourseWork.Models.AutoDealership.AutoDealership autodealership)
        {
            OnAutoDealershipUpdated(autodealership);

            var itemToUpdate = Context.AutoDealerships
                .FirstOrDefault(i => i.Id == autodealership.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(autodealership);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAutoDealershipUpdated(autodealership);

            return autodealership;
        }

        partial void OnAutoDealershipDeleted(CourseWork.Models.AutoDealership.AutoDealership item);
        partial void OnAfterAutoDealershipDeleted(CourseWork.Models.AutoDealership.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealership.AutoDealership> DeleteAutoDealership(int id)
        {
            var itemToDelete = Context.AutoDealerships
                              .Where(i => i.Id == id)
                              .Include(i => i.DealershipCars)
                              .Include(i => i.Employees)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnAutoDealershipDeleted(itemToDelete);


            Context.AutoDealerships.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAutoDealershipDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportBrandsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/brands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/brands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBrandsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/brands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/brands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBrandsRead(ref IQueryable<CourseWork.Models.AutoDealership.Brand> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Brand>> GetBrands(Query query = null)
        {
            var items = Context.Brands.AsQueryable();

            items = items.Include(i => i.Country);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBrandsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBrandGet(CourseWork.Models.AutoDealership.Brand item);
        partial void OnGetBrandById(ref IQueryable<CourseWork.Models.AutoDealership.Brand> items);


        public async Task<CourseWork.Models.AutoDealership.Brand> GetBrandById(int id)
        {
            var items = Context.Brands
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Country);

            OnGetBrandById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBrandGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBrandCreated(CourseWork.Models.AutoDealership.Brand item);
        partial void OnAfterBrandCreated(CourseWork.Models.AutoDealership.Brand item);

        public async Task<CourseWork.Models.AutoDealership.Brand> CreateBrand(CourseWork.Models.AutoDealership.Brand brand)
        {
            OnBrandCreated(brand);

            var existingItem = Context.Brands
                .FirstOrDefault(i => i.Id == brand.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Brands.Add(brand);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(brand).State = EntityState.Detached;
                throw;
            }

            OnAfterBrandCreated(brand);

            return brand;
        }

        public async Task<CourseWork.Models.AutoDealership.Brand> CancelBrandChanges(CourseWork.Models.AutoDealership.Brand item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBrandUpdated(CourseWork.Models.AutoDealership.Brand item);
        partial void OnAfterBrandUpdated(CourseWork.Models.AutoDealership.Brand item);

        public async Task<CourseWork.Models.AutoDealership.Brand> UpdateBrand(int id, CourseWork.Models.AutoDealership.Brand brand)
        {
            OnBrandUpdated(brand);

            var itemToUpdate = Context.Brands
                .FirstOrDefault(i => i.Id == brand.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(brand);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBrandUpdated(brand);

            return brand;
        }

        partial void OnBrandDeleted(CourseWork.Models.AutoDealership.Brand item);
        partial void OnAfterBrandDeleted(CourseWork.Models.AutoDealership.Brand item);

        public async Task<CourseWork.Models.AutoDealership.Brand> DeleteBrand(int id)
        {
            var itemToDelete = Context.Brands
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .Include(i => i.Engines)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnBrandDeleted(itemToDelete);


            Context.Brands.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBrandDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarBodyTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carbodytypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carbodytypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarBodyTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carbodytypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carbodytypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarBodyTypesRead(ref IQueryable<CourseWork.Models.AutoDealership.CarBodyType> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarBodyType>> GetCarBodyTypes(Query query = null)
        {
            var items = Context.CarBodyTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarBodyTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarBodyTypeGet(CourseWork.Models.AutoDealership.CarBodyType item);
        partial void OnGetCarBodyTypeById(ref IQueryable<CourseWork.Models.AutoDealership.CarBodyType> items);


        public async Task<CourseWork.Models.AutoDealership.CarBodyType> GetCarBodyTypeById(int id)
        {
            var items = Context.CarBodyTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetCarBodyTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarBodyTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarBodyTypeCreated(CourseWork.Models.AutoDealership.CarBodyType item);
        partial void OnAfterCarBodyTypeCreated(CourseWork.Models.AutoDealership.CarBodyType item);

        public async Task<CourseWork.Models.AutoDealership.CarBodyType> CreateCarBodyType(CourseWork.Models.AutoDealership.CarBodyType carbodytype)
        {
            OnCarBodyTypeCreated(carbodytype);

            var existingItem = Context.CarBodyTypes
                .FirstOrDefault(i => i.Id == carbodytype.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarBodyTypes.Add(carbodytype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carbodytype).State = EntityState.Detached;
                throw;
            }

            OnAfterCarBodyTypeCreated(carbodytype);

            return carbodytype;
        }

        public async Task<CourseWork.Models.AutoDealership.CarBodyType> CancelCarBodyTypeChanges(CourseWork.Models.AutoDealership.CarBodyType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarBodyTypeUpdated(CourseWork.Models.AutoDealership.CarBodyType item);
        partial void OnAfterCarBodyTypeUpdated(CourseWork.Models.AutoDealership.CarBodyType item);

        public async Task<CourseWork.Models.AutoDealership.CarBodyType> UpdateCarBodyType(int id, CourseWork.Models.AutoDealership.CarBodyType carbodytype)
        {
            OnCarBodyTypeUpdated(carbodytype);

            var itemToUpdate = Context.CarBodyTypes
                .FirstOrDefault(i => i.Id == carbodytype.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carbodytype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarBodyTypeUpdated(carbodytype);

            return carbodytype;
        }

        partial void OnCarBodyTypeDeleted(CourseWork.Models.AutoDealership.CarBodyType item);
        partial void OnAfterCarBodyTypeDeleted(CourseWork.Models.AutoDealership.CarBodyType item);

        public async Task<CourseWork.Models.AutoDealership.CarBodyType> DeleteCarBodyType(int id)
        {
            var itemToDelete = Context.CarBodyTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarBodyTypeDeleted(itemToDelete);


            Context.CarBodyTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarBodyTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarComfortOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carcomfortoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carcomfortoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarComfortOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carcomfortoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carcomfortoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarComfortOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.CarComfortOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarComfortOption>> GetCarComfortOptions(Query query = null)
        {
            var items = Context.CarComfortOptions.AsQueryable();

            items = items.Include(i => i.Car);
            items = items.Include(i => i.ComfortOption);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarComfortOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarComfortOptionGet(CourseWork.Models.AutoDealership.CarComfortOption item);
        partial void OnGetCarComfortOptionByCarIdAndComfortOptionId(ref IQueryable<CourseWork.Models.AutoDealership.CarComfortOption> items);


        public async Task<CourseWork.Models.AutoDealership.CarComfortOption> GetCarComfortOptionByCarIdAndComfortOptionId(int carid, int comfortoptionid)
        {
            var items = Context.CarComfortOptions
                              .AsNoTracking()
                              .Where(i => i.CarId == carid && i.ComfortOptionId == comfortoptionid);

            items = items.Include(i => i.Car);
            items = items.Include(i => i.ComfortOption);

            OnGetCarComfortOptionByCarIdAndComfortOptionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarComfortOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarComfortOptionCreated(CourseWork.Models.AutoDealership.CarComfortOption item);
        partial void OnAfterCarComfortOptionCreated(CourseWork.Models.AutoDealership.CarComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.CarComfortOption> CreateCarComfortOption(CourseWork.Models.AutoDealership.CarComfortOption carcomfortoption)
        {
            OnCarComfortOptionCreated(carcomfortoption);

            var existingItem = Context.CarComfortOptions
                .FirstOrDefault(i => i.CarId == carcomfortoption.CarId && i.ComfortOptionId == carcomfortoption.ComfortOptionId);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarComfortOptions.Add(carcomfortoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carcomfortoption).State = EntityState.Detached;
                throw;
            }

            OnAfterCarComfortOptionCreated(carcomfortoption);

            return carcomfortoption;
        }

        public async Task<CourseWork.Models.AutoDealership.CarComfortOption> CancelCarComfortOptionChanges(CourseWork.Models.AutoDealership.CarComfortOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarComfortOptionUpdated(CourseWork.Models.AutoDealership.CarComfortOption item);
        partial void OnAfterCarComfortOptionUpdated(CourseWork.Models.AutoDealership.CarComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.CarComfortOption> UpdateCarComfortOption(int carid, int comfortoptionid, CourseWork.Models.AutoDealership.CarComfortOption carcomfortoption)
        {
            OnCarComfortOptionUpdated(carcomfortoption);

            var itemToUpdate = Context.CarComfortOptions
                .FirstOrDefault(i => i.CarId == carcomfortoption.CarId && i.ComfortOptionId == carcomfortoption.ComfortOptionId);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carcomfortoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarComfortOptionUpdated(carcomfortoption);

            return carcomfortoption;
        }

        partial void OnCarComfortOptionDeleted(CourseWork.Models.AutoDealership.CarComfortOption item);
        partial void OnAfterCarComfortOptionDeleted(CourseWork.Models.AutoDealership.CarComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.CarComfortOption> DeleteCarComfortOption(int carid, int comfortoptionid)
        {
            var itemToDelete = Context.CarComfortOptions
                .FirstOrDefault(i => i.CarId == carid && i.ComfortOptionId == comfortoptionid);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarComfortOptionDeleted(itemToDelete);


            Context.CarComfortOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarComfortOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarDeliveriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cardeliveries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cardeliveries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarDeliveriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cardeliveries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cardeliveries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarDeliveriesRead(ref IQueryable<CourseWork.Models.AutoDealership.CarDelivery> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarDelivery>> GetCarDeliveries(Query query = null)
        {
            var items = Context.CarDeliveries.AsQueryable();

            items = items.Include(i => i.Distributor);
            items = items.Include(i => i.CarSale);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarDeliveriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarDeliveryGet(CourseWork.Models.AutoDealership.CarDelivery item);
        partial void OnGetCarDeliveryById(ref IQueryable<CourseWork.Models.AutoDealership.CarDelivery> items);


        public async Task<CourseWork.Models.AutoDealership.CarDelivery> GetCarDeliveryById(int id)
        {
            var items = Context.CarDeliveries
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Distributor);
            items = items.Include(i => i.CarSale);

            OnGetCarDeliveryById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarDeliveryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarDeliveryCreated(CourseWork.Models.AutoDealership.CarDelivery item);
        partial void OnAfterCarDeliveryCreated(CourseWork.Models.AutoDealership.CarDelivery item);

        public async Task<CourseWork.Models.AutoDealership.CarDelivery> CreateCarDelivery(CourseWork.Models.AutoDealership.CarDelivery cardelivery)
        {
            OnCarDeliveryCreated(cardelivery);

            var existingItem = Context.CarDeliveries
                .FirstOrDefault(i => i.Id == cardelivery.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarDeliveries.Add(cardelivery);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cardelivery).State = EntityState.Detached;
                throw;
            }

            OnAfterCarDeliveryCreated(cardelivery);

            return cardelivery;
        }

        public async Task<CourseWork.Models.AutoDealership.CarDelivery> CancelCarDeliveryChanges(CourseWork.Models.AutoDealership.CarDelivery item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarDeliveryUpdated(CourseWork.Models.AutoDealership.CarDelivery item);
        partial void OnAfterCarDeliveryUpdated(CourseWork.Models.AutoDealership.CarDelivery item);

        public async Task<CourseWork.Models.AutoDealership.CarDelivery> UpdateCarDelivery(int id, CourseWork.Models.AutoDealership.CarDelivery cardelivery)
        {
            OnCarDeliveryUpdated(cardelivery);

            var itemToUpdate = Context.CarDeliveries
                .FirstOrDefault(i => i.Id == cardelivery.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cardelivery);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarDeliveryUpdated(cardelivery);

            return cardelivery;
        }

        partial void OnCarDeliveryDeleted(CourseWork.Models.AutoDealership.CarDelivery item);
        partial void OnAfterCarDeliveryDeleted(CourseWork.Models.AutoDealership.CarDelivery item);

        public async Task<CourseWork.Models.AutoDealership.CarDelivery> DeleteCarDelivery(int id)
        {
            var itemToDelete = Context.CarDeliveries
                .FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarDeliveryDeleted(itemToDelete);


            Context.CarDeliveries.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarDeliveryDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarMultimediaOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carmultimediaoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carmultimediaoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarMultimediaOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carmultimediaoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carmultimediaoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarMultimediaOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.CarMultimediaOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarMultimediaOption>> GetCarMultimediaOptions(Query query = null)
        {
            var items = Context.CarMultimediaOptions.AsQueryable();

            items = items.Include(i => i.Car);
            items = items.Include(i => i.MultimediaOption);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarMultimediaOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarMultimediaOptionGet(CourseWork.Models.AutoDealership.CarMultimediaOption item);
        partial void OnGetCarMultimediaOptionByCarIdAndMultimediaOptionId(ref IQueryable<CourseWork.Models.AutoDealership.CarMultimediaOption> items);


        public async Task<CourseWork.Models.AutoDealership.CarMultimediaOption> GetCarMultimediaOptionByCarIdAndMultimediaOptionId(int carid, int multimediaoptionid)
        {
            var items = Context.CarMultimediaOptions
                              .AsNoTracking()
                              .Where(i => i.CarId == carid && i.MultimediaOptionId == multimediaoptionid);

            items = items.Include(i => i.Car);
            items = items.Include(i => i.MultimediaOption);

            OnGetCarMultimediaOptionByCarIdAndMultimediaOptionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarMultimediaOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarMultimediaOptionCreated(CourseWork.Models.AutoDealership.CarMultimediaOption item);
        partial void OnAfterCarMultimediaOptionCreated(CourseWork.Models.AutoDealership.CarMultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.CarMultimediaOption> CreateCarMultimediaOption(CourseWork.Models.AutoDealership.CarMultimediaOption carmultimediaoption)
        {
            OnCarMultimediaOptionCreated(carmultimediaoption);

            var existingItem = Context.CarMultimediaOptions
                .FirstOrDefault(i => i.CarId == carmultimediaoption.CarId && i.MultimediaOptionId == carmultimediaoption.MultimediaOptionId);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarMultimediaOptions.Add(carmultimediaoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carmultimediaoption).State = EntityState.Detached;
                throw;
            }

            OnAfterCarMultimediaOptionCreated(carmultimediaoption);

            return carmultimediaoption;
        }

        public async Task<CourseWork.Models.AutoDealership.CarMultimediaOption> CancelCarMultimediaOptionChanges(CourseWork.Models.AutoDealership.CarMultimediaOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarMultimediaOptionUpdated(CourseWork.Models.AutoDealership.CarMultimediaOption item);
        partial void OnAfterCarMultimediaOptionUpdated(CourseWork.Models.AutoDealership.CarMultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.CarMultimediaOption> UpdateCarMultimediaOption(int carid, int multimediaoptionid, CourseWork.Models.AutoDealership.CarMultimediaOption carmultimediaoption)
        {
            OnCarMultimediaOptionUpdated(carmultimediaoption);

            var itemToUpdate = Context.CarMultimediaOptions
                .FirstOrDefault(i => i.CarId == carmultimediaoption.CarId && i.MultimediaOptionId == carmultimediaoption.MultimediaOptionId);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carmultimediaoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarMultimediaOptionUpdated(carmultimediaoption);

            return carmultimediaoption;
        }

        partial void OnCarMultimediaOptionDeleted(CourseWork.Models.AutoDealership.CarMultimediaOption item);
        partial void OnAfterCarMultimediaOptionDeleted(CourseWork.Models.AutoDealership.CarMultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.CarMultimediaOption> DeleteCarMultimediaOption(int carid, int multimediaoptionid)
        {
            var itemToDelete = Context.CarMultimediaOptions
                .FirstOrDefault(i => i.CarId == carid && i.MultimediaOptionId == multimediaoptionid);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarMultimediaOptionDeleted(itemToDelete);


            Context.CarMultimediaOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarMultimediaOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarsRead(ref IQueryable<CourseWork.Models.AutoDealership.Car> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Car>> GetCars(Query query = null)
        {
            var items = Context.Cars.AsQueryable();

            items = items.Include(i => i.CarBodyType);
            items = items.Include(i => i.Brand);
            items = items.Include(i => i.Color);
            items = items.Include(i => i.Engine);
            items = items.Include(i => i.GearBoxType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarGet(CourseWork.Models.AutoDealership.Car item);
        partial void OnGetCarById(ref IQueryable<CourseWork.Models.AutoDealership.Car> items);


        public async Task<CourseWork.Models.AutoDealership.Car> GetCarById(int id)
        {
            var items = Context.Cars
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CarBodyType);
            items = items.Include(i => i.Brand);
            items = items.Include(i => i.Color);
            items = items.Include(i => i.Engine);
            items = items.Include(i => i.GearBoxType);

            OnGetCarById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarCreated(CourseWork.Models.AutoDealership.Car item);
        partial void OnAfterCarCreated(CourseWork.Models.AutoDealership.Car item);

        public async Task<CourseWork.Models.AutoDealership.Car> CreateCar(CourseWork.Models.AutoDealership.Car car)
        {
            OnCarCreated(car);

            var existingItem = Context.Cars
                .FirstOrDefault(i => i.Id == car.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Cars.Add(car);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(car).State = EntityState.Detached;
                throw;
            }

            OnAfterCarCreated(car);

            return car;
        }

        public async Task<CourseWork.Models.AutoDealership.Car> CancelCarChanges(CourseWork.Models.AutoDealership.Car item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarUpdated(CourseWork.Models.AutoDealership.Car item);
        partial void OnAfterCarUpdated(CourseWork.Models.AutoDealership.Car item);

        public async Task<CourseWork.Models.AutoDealership.Car> UpdateCar(int id, CourseWork.Models.AutoDealership.Car car)
        {
            OnCarUpdated(car);

            var itemToUpdate = Context.Cars
                .FirstOrDefault(i => i.Id == car.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(car);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarUpdated(car);

            return car;
        }

        partial void OnCarDeleted(CourseWork.Models.AutoDealership.Car item);
        partial void OnAfterCarDeleted(CourseWork.Models.AutoDealership.Car item);

        public async Task<CourseWork.Models.AutoDealership.Car> DeleteCar(int id)
        {
            var itemToDelete = Context.Cars
                              .Where(i => i.Id == id)
                              .Include(i => i.CarComfortOptions)
                              .Include(i => i.CarMultimediaOptions)
                              .Include(i => i.CarSafetyOptions)
                              .Include(i => i.DealershipCars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarDeleted(itemToDelete);


            Context.Cars.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarSafetyOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carsafetyoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carsafetyoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarSafetyOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carsafetyoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carsafetyoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarSafetyOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.CarSafetyOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarSafetyOption>> GetCarSafetyOptions(Query query = null)
        {
            var items = Context.CarSafetyOptions.AsQueryable();

            items = items.Include(i => i.Car);
            items = items.Include(i => i.SafetyOption);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarSafetyOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarSafetyOptionGet(CourseWork.Models.AutoDealership.CarSafetyOption item);
        partial void OnGetCarSafetyOptionByCarIdAndSafetyOptionId(ref IQueryable<CourseWork.Models.AutoDealership.CarSafetyOption> items);


        public async Task<CourseWork.Models.AutoDealership.CarSafetyOption> GetCarSafetyOptionByCarIdAndSafetyOptionId(int carid, int safetyoptionid)
        {
            var items = Context.CarSafetyOptions
                              .AsNoTracking()
                              .Where(i => i.CarId == carid && i.SafetyOptionId == safetyoptionid);

            items = items.Include(i => i.Car);
            items = items.Include(i => i.SafetyOption);

            OnGetCarSafetyOptionByCarIdAndSafetyOptionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarSafetyOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarSafetyOptionCreated(CourseWork.Models.AutoDealership.CarSafetyOption item);
        partial void OnAfterCarSafetyOptionCreated(CourseWork.Models.AutoDealership.CarSafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.CarSafetyOption> CreateCarSafetyOption(CourseWork.Models.AutoDealership.CarSafetyOption carsafetyoption)
        {
            OnCarSafetyOptionCreated(carsafetyoption);

            var existingItem = Context.CarSafetyOptions
                .FirstOrDefault(i => i.CarId == carsafetyoption.CarId && i.SafetyOptionId == carsafetyoption.SafetyOptionId);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarSafetyOptions.Add(carsafetyoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carsafetyoption).State = EntityState.Detached;
                throw;
            }

            OnAfterCarSafetyOptionCreated(carsafetyoption);

            return carsafetyoption;
        }

        public async Task<CourseWork.Models.AutoDealership.CarSafetyOption> CancelCarSafetyOptionChanges(CourseWork.Models.AutoDealership.CarSafetyOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarSafetyOptionUpdated(CourseWork.Models.AutoDealership.CarSafetyOption item);
        partial void OnAfterCarSafetyOptionUpdated(CourseWork.Models.AutoDealership.CarSafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.CarSafetyOption> UpdateCarSafetyOption(int carid, int safetyoptionid, CourseWork.Models.AutoDealership.CarSafetyOption carsafetyoption)
        {
            OnCarSafetyOptionUpdated(carsafetyoption);

            var itemToUpdate = Context.CarSafetyOptions
                .FirstOrDefault(i => i.CarId == carsafetyoption.CarId && i.SafetyOptionId == carsafetyoption.SafetyOptionId);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carsafetyoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarSafetyOptionUpdated(carsafetyoption);

            return carsafetyoption;
        }

        partial void OnCarSafetyOptionDeleted(CourseWork.Models.AutoDealership.CarSafetyOption item);
        partial void OnAfterCarSafetyOptionDeleted(CourseWork.Models.AutoDealership.CarSafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.CarSafetyOption> DeleteCarSafetyOption(int carid, int safetyoptionid)
        {
            var itemToDelete = Context.CarSafetyOptions
                .FirstOrDefault(i => i.CarId == carid && i.SafetyOptionId == safetyoptionid);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarSafetyOptionDeleted(itemToDelete);


            Context.CarSafetyOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarSafetyOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarSalesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarSalesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/carsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/carsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarSalesRead(ref IQueryable<CourseWork.Models.AutoDealership.CarSale> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarSale>> GetCarSales(Query query = null)
        {
            var items = Context.CarSales.AsQueryable();

            items = items.Include(i => i.Customer);
            items = items.Include(i => i.DealershipCar);
            items = items.Include(i => i.Employee);
            items = items.Include(i => i.PaymentMethod);
            items = items.Include(i => i.SaleStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarSalesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarSaleGet(CourseWork.Models.AutoDealership.CarSale item);
        partial void OnGetCarSaleById(ref IQueryable<CourseWork.Models.AutoDealership.CarSale> items);


        public async Task<CourseWork.Models.AutoDealership.CarSale> GetCarSaleById(int id)
        {
            var items = Context.CarSales
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Customer);
            items = items.Include(i => i.DealershipCar);
            items = items.Include(i => i.Employee);
            items = items.Include(i => i.PaymentMethod);
            items = items.Include(i => i.SaleStatus);

            OnGetCarSaleById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarSaleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarSaleCreated(CourseWork.Models.AutoDealership.CarSale item);
        partial void OnAfterCarSaleCreated(CourseWork.Models.AutoDealership.CarSale item);

        public async Task<CourseWork.Models.AutoDealership.CarSale> CreateCarSale(CourseWork.Models.AutoDealership.CarSale carsale)
        {
            OnCarSaleCreated(carsale);

            try
            {
                carsale.SaleDate = DateTime.Now;
                carsale.CreateDate = DateTime.Now;
                carsale.UpdateDate = DateTime.Now;
                Context.CarSales.Add(carsale);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carsale).State = EntityState.Detached;
                throw;
            }

            OnAfterCarSaleCreated(carsale);

            return carsale;
        }

        public async Task<CourseWork.Models.AutoDealership.CarSale> CancelCarSaleChanges(CourseWork.Models.AutoDealership.CarSale item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarSaleUpdated(CourseWork.Models.AutoDealership.CarSale item);
        partial void OnAfterCarSaleUpdated(CourseWork.Models.AutoDealership.CarSale item);

        public async Task<CourseWork.Models.AutoDealership.CarSale> UpdateCarSale(int id, CourseWork.Models.AutoDealership.CarSale carsale)
        {
            OnCarSaleUpdated(carsale);

            var itemToUpdate = Context.CarSales
                .FirstOrDefault(i => i.Id == carsale.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carsale);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarSaleUpdated(carsale);

            return carsale;
        }

        partial void OnCarSaleDeleted(CourseWork.Models.AutoDealership.CarSale item);
        partial void OnAfterCarSaleDeleted(CourseWork.Models.AutoDealership.CarSale item);

        public async Task<CourseWork.Models.AutoDealership.CarSale> DeleteCarSale(int id)
        {
            var itemToDelete = Context.CarSales
                              .Where(i => i.Id == id)
                              .Include(i => i.CarDeliveries)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarSaleDeleted(itemToDelete);


            Context.CarSales.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarSaleDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCarTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cartypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cartypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cartypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cartypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarTypesRead(ref IQueryable<CourseWork.Models.AutoDealership.CarType> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.CarType>> GetCarTypes(Query query = null)
        {
            var items = Context.CarTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarTypeGet(CourseWork.Models.AutoDealership.CarType item);
        partial void OnGetCarTypeById(ref IQueryable<CourseWork.Models.AutoDealership.CarType> items);


        public async Task<CourseWork.Models.AutoDealership.CarType> GetCarTypeById(int id)
        {
            var items = Context.CarTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetCarTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarTypeCreated(CourseWork.Models.AutoDealership.CarType item);
        partial void OnAfterCarTypeCreated(CourseWork.Models.AutoDealership.CarType item);

        public async Task<CourseWork.Models.AutoDealership.CarType> CreateCarType(CourseWork.Models.AutoDealership.CarType cartype)
        {
            OnCarTypeCreated(cartype);

            var existingItem = Context.CarTypes
                .FirstOrDefault(i => i.Id == cartype.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.CarTypes.Add(cartype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cartype).State = EntityState.Detached;
                throw;
            }

            OnAfterCarTypeCreated(cartype);

            return cartype;
        }

        public async Task<CourseWork.Models.AutoDealership.CarType> CancelCarTypeChanges(CourseWork.Models.AutoDealership.CarType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarTypeUpdated(CourseWork.Models.AutoDealership.CarType item);
        partial void OnAfterCarTypeUpdated(CourseWork.Models.AutoDealership.CarType item);

        public async Task<CourseWork.Models.AutoDealership.CarType> UpdateCarType(int id, CourseWork.Models.AutoDealership.CarType cartype)
        {
            OnCarTypeUpdated(cartype);

            var itemToUpdate = Context.CarTypes
                .FirstOrDefault(i => i.Id == cartype.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cartype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarTypeUpdated(cartype);

            return cartype;
        }

        partial void OnCarTypeDeleted(CourseWork.Models.AutoDealership.CarType item);
        partial void OnAfterCarTypeDeleted(CourseWork.Models.AutoDealership.CarType item);

        public async Task<CourseWork.Models.AutoDealership.CarType> DeleteCarType(int id)
        {
            var itemToDelete = Context.CarTypes
                .FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCarTypeDeleted(itemToDelete);


            Context.CarTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCitiesRead(ref IQueryable<CourseWork.Models.AutoDealership.City> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.City>> GetCities(Query query = null)
        {
            var items = Context.Cities.AsQueryable();

            items = items.Include(i => i.Country);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCityGet(CourseWork.Models.AutoDealership.City item);
        partial void OnGetCityById(ref IQueryable<CourseWork.Models.AutoDealership.City> items);


        public async Task<CourseWork.Models.AutoDealership.City> GetCityById(int id)
        {
            var items = Context.Cities
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Country);

            OnGetCityById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCityCreated(CourseWork.Models.AutoDealership.City item);
        partial void OnAfterCityCreated(CourseWork.Models.AutoDealership.City item);

        public async Task<CourseWork.Models.AutoDealership.City> CreateCity(CourseWork.Models.AutoDealership.City city)
        {
            OnCityCreated(city);

            var existingItem = Context.Cities
                .FirstOrDefault(i => i.Id == city.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Cities.Add(city);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(city).State = EntityState.Detached;
                throw;
            }

            OnAfterCityCreated(city);

            return city;
        }

        public async Task<CourseWork.Models.AutoDealership.City> CancelCityChanges(CourseWork.Models.AutoDealership.City item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCityUpdated(CourseWork.Models.AutoDealership.City item);
        partial void OnAfterCityUpdated(CourseWork.Models.AutoDealership.City item);

        public async Task<CourseWork.Models.AutoDealership.City> UpdateCity(int id, CourseWork.Models.AutoDealership.City city)
        {
            OnCityUpdated(city);

            var itemToUpdate = Context.Cities
                .FirstOrDefault(i => i.Id == city.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(city);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCityUpdated(city);

            return city;
        }

        partial void OnCityDeleted(CourseWork.Models.AutoDealership.City item);
        partial void OnAfterCityDeleted(CourseWork.Models.AutoDealership.City item);

        public async Task<CourseWork.Models.AutoDealership.City> DeleteCity(int id)
        {
            var itemToDelete = Context.Cities
                              .Where(i => i.Id == id)
                              .Include(i => i.AutoDealerships)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCityDeleted(itemToDelete);


            Context.Cities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCityDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportColorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/colors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/colors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/colors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/colors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColorsRead(ref IQueryable<CourseWork.Models.AutoDealership.Color> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Color>> GetColors(Query query = null)
        {
            var items = Context.Colors.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnColorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColorGet(CourseWork.Models.AutoDealership.Color item);
        partial void OnGetColorById(ref IQueryable<CourseWork.Models.AutoDealership.Color> items);


        public async Task<CourseWork.Models.AutoDealership.Color> GetColorById(int id)
        {
            var items = Context.Colors
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetColorById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColorCreated(CourseWork.Models.AutoDealership.Color item);
        partial void OnAfterColorCreated(CourseWork.Models.AutoDealership.Color item);

        public async Task<CourseWork.Models.AutoDealership.Color> CreateColor(CourseWork.Models.AutoDealership.Color color)
        {
            OnColorCreated(color);

            var existingItem = Context.Colors
                .FirstOrDefault(i => i.Id == color.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Colors.Add(color);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(color).State = EntityState.Detached;
                throw;
            }

            OnAfterColorCreated(color);

            return color;
        }

        public async Task<CourseWork.Models.AutoDealership.Color> CancelColorChanges(CourseWork.Models.AutoDealership.Color item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColorUpdated(CourseWork.Models.AutoDealership.Color item);
        partial void OnAfterColorUpdated(CourseWork.Models.AutoDealership.Color item);

        public async Task<CourseWork.Models.AutoDealership.Color> UpdateColor(int id, CourseWork.Models.AutoDealership.Color color)
        {
            OnColorUpdated(color);

            var itemToUpdate = Context.Colors
                .FirstOrDefault(i => i.Id == color.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(color);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColorUpdated(color);

            return color;
        }

        partial void OnColorDeleted(CourseWork.Models.AutoDealership.Color item);
        partial void OnAfterColorDeleted(CourseWork.Models.AutoDealership.Color item);

        public async Task<CourseWork.Models.AutoDealership.Color> DeleteColor(int id)
        {
            var itemToDelete = Context.Colors
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnColorDeleted(itemToDelete);


            Context.Colors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColorDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportComfortOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/comfortoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/comfortoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportComfortOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/comfortoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/comfortoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnComfortOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.ComfortOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.ComfortOption>> GetComfortOptions(Query query = null)
        {
            var items = Context.ComfortOptions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnComfortOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnComfortOptionGet(CourseWork.Models.AutoDealership.ComfortOption item);
        partial void OnGetComfortOptionById(ref IQueryable<CourseWork.Models.AutoDealership.ComfortOption> items);


        public async Task<CourseWork.Models.AutoDealership.ComfortOption> GetComfortOptionById(int id)
        {
            var items = Context.ComfortOptions
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetComfortOptionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnComfortOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnComfortOptionCreated(CourseWork.Models.AutoDealership.ComfortOption item);
        partial void OnAfterComfortOptionCreated(CourseWork.Models.AutoDealership.ComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.ComfortOption> CreateComfortOption(CourseWork.Models.AutoDealership.ComfortOption comfortoption)
        {
            OnComfortOptionCreated(comfortoption);

            var existingItem = Context.ComfortOptions
                .FirstOrDefault(i => i.Id == comfortoption.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.ComfortOptions.Add(comfortoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(comfortoption).State = EntityState.Detached;
                throw;
            }

            OnAfterComfortOptionCreated(comfortoption);

            return comfortoption;
        }

        public async Task<CourseWork.Models.AutoDealership.ComfortOption> CancelComfortOptionChanges(CourseWork.Models.AutoDealership.ComfortOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnComfortOptionUpdated(CourseWork.Models.AutoDealership.ComfortOption item);
        partial void OnAfterComfortOptionUpdated(CourseWork.Models.AutoDealership.ComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.ComfortOption> UpdateComfortOption(int id, CourseWork.Models.AutoDealership.ComfortOption comfortoption)
        {
            OnComfortOptionUpdated(comfortoption);

            var itemToUpdate = Context.ComfortOptions
                .FirstOrDefault(i => i.Id == comfortoption.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(comfortoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterComfortOptionUpdated(comfortoption);

            return comfortoption;
        }

        partial void OnComfortOptionDeleted(CourseWork.Models.AutoDealership.ComfortOption item);
        partial void OnAfterComfortOptionDeleted(CourseWork.Models.AutoDealership.ComfortOption item);

        public async Task<CourseWork.Models.AutoDealership.ComfortOption> DeleteComfortOption(int id)
        {
            var itemToDelete = Context.ComfortOptions
                              .Where(i => i.Id == id)
                              .Include(i => i.CarComfortOptions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnComfortOptionDeleted(itemToDelete);


            Context.ComfortOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterComfortOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportConditionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/conditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/conditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportConditionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/conditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/conditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnConditionsRead(ref IQueryable<CourseWork.Models.AutoDealership.Condition> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Condition>> GetConditions(Query query = null)
        {
            var items = Context.Conditions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnConditionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnConditionGet(CourseWork.Models.AutoDealership.Condition item);
        partial void OnGetConditionById(ref IQueryable<CourseWork.Models.AutoDealership.Condition> items);


        public async Task<CourseWork.Models.AutoDealership.Condition> GetConditionById(int id)
        {
            var items = Context.Conditions
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetConditionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnConditionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnConditionCreated(CourseWork.Models.AutoDealership.Condition item);
        partial void OnAfterConditionCreated(CourseWork.Models.AutoDealership.Condition item);

        public async Task<CourseWork.Models.AutoDealership.Condition> CreateCondition(CourseWork.Models.AutoDealership.Condition condition)
        {
            OnConditionCreated(condition);

            var existingItem = Context.Conditions
                .FirstOrDefault(i => i.Id == condition.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Conditions.Add(condition);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(condition).State = EntityState.Detached;
                throw;
            }

            OnAfterConditionCreated(condition);

            return condition;
        }

        public async Task<CourseWork.Models.AutoDealership.Condition> CancelConditionChanges(CourseWork.Models.AutoDealership.Condition item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnConditionUpdated(CourseWork.Models.AutoDealership.Condition item);
        partial void OnAfterConditionUpdated(CourseWork.Models.AutoDealership.Condition item);

        public async Task<CourseWork.Models.AutoDealership.Condition> UpdateCondition(int id, CourseWork.Models.AutoDealership.Condition condition)
        {
            OnConditionUpdated(condition);

            var itemToUpdate = Context.Conditions
                .FirstOrDefault(i => i.Id == condition.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(condition);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterConditionUpdated(condition);

            return condition;
        }

        partial void OnConditionDeleted(CourseWork.Models.AutoDealership.Condition item);
        partial void OnAfterConditionDeleted(CourseWork.Models.AutoDealership.Condition item);

        public async Task<CourseWork.Models.AutoDealership.Condition> DeleteCondition(int id)
        {
            var itemToDelete = Context.Conditions
                              .Where(i => i.Id == id)
                              .Include(i => i.LeaseProposalConditions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnConditionDeleted(itemToDelete);


            Context.Conditions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterConditionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCountriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/countries/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCountriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/countries/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCountriesRead(ref IQueryable<CourseWork.Models.AutoDealership.Country> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Country>> GetCountries(Query query = null)
        {
            var items = Context.Countries.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCountriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCountryGet(CourseWork.Models.AutoDealership.Country item);
        partial void OnGetCountryById(ref IQueryable<CourseWork.Models.AutoDealership.Country> items);


        public async Task<CourseWork.Models.AutoDealership.Country> GetCountryById(int id)
        {
            var items = Context.Countries
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetCountryById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCountryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCountryCreated(CourseWork.Models.AutoDealership.Country item);
        partial void OnAfterCountryCreated(CourseWork.Models.AutoDealership.Country item);

        public async Task<CourseWork.Models.AutoDealership.Country> CreateCountry(CourseWork.Models.AutoDealership.Country country)
        {
            OnCountryCreated(country);

            var existingItem = Context.Countries
                .FirstOrDefault(i => i.Id == country.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Countries.Add(country);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(country).State = EntityState.Detached;
                throw;
            }

            OnAfterCountryCreated(country);

            return country;
        }

        public async Task<CourseWork.Models.AutoDealership.Country> CancelCountryChanges(CourseWork.Models.AutoDealership.Country item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCountryUpdated(CourseWork.Models.AutoDealership.Country item);
        partial void OnAfterCountryUpdated(CourseWork.Models.AutoDealership.Country item);

        public async Task<CourseWork.Models.AutoDealership.Country> UpdateCountry(int id, CourseWork.Models.AutoDealership.Country country)
        {
            OnCountryUpdated(country);

            var itemToUpdate = Context.Countries
                .FirstOrDefault(i => i.Id == country.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(country);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCountryUpdated(country);

            return country;
        }

        partial void OnCountryDeleted(CourseWork.Models.AutoDealership.Country item);
        partial void OnAfterCountryDeleted(CourseWork.Models.AutoDealership.Country item);

        public async Task<CourseWork.Models.AutoDealership.Country> DeleteCountry(int id)
        {
            var itemToDelete = Context.Countries
                              .Where(i => i.Id == id)
                              .Include(i => i.Brands)
                              .Include(i => i.Cities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCountryDeleted(itemToDelete);


            Context.Countries.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCountryDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomersRead(ref IQueryable<CourseWork.Models.AutoDealership.Customer> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Customer>> GetCustomers(Query query = null)
        {
            var items = Context.Customers.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCustomersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerGet(CourseWork.Models.AutoDealership.Customer item);
        partial void OnGetCustomerById(ref IQueryable<CourseWork.Models.AutoDealership.Customer> items);


        public async Task<CourseWork.Models.AutoDealership.Customer> GetCustomerById(int id)
        {
            var items = Context.Customers
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetCustomerById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCustomerCreated(CourseWork.Models.AutoDealership.Customer item);
        partial void OnAfterCustomerCreated(CourseWork.Models.AutoDealership.Customer item);

        public async Task<CourseWork.Models.AutoDealership.Customer> CreateCustomer(CourseWork.Models.AutoDealership.Customer customer)
        {
            OnCustomerCreated(customer);

            try
            {

                customer.Id = await Context.Customers.Select(c => c.Id).MaxAsync() + 1;
                customer.CreateDate = DateTime.Now;
                customer.UpdateDate = DateTime.Now;

                await Context.Customers.AddAsync(customer);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(customer).State = EntityState.Detached;
                throw;
            }

            OnAfterCustomerCreated(customer);

            return customer;
        }

        public async Task<CourseWork.Models.AutoDealership.Customer> CancelCustomerChanges(CourseWork.Models.AutoDealership.Customer item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCustomerUpdated(CourseWork.Models.AutoDealership.Customer item);
        partial void OnAfterCustomerUpdated(CourseWork.Models.AutoDealership.Customer item);

        public async Task<CourseWork.Models.AutoDealership.Customer> UpdateCustomer(int id, CourseWork.Models.AutoDealership.Customer customer)
        {
            OnCustomerUpdated(customer);

            var itemToUpdate = Context.Customers
                .FirstOrDefault(i => i.Id == customer.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customer);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCustomerUpdated(customer);

            return customer;
        }

        partial void OnCustomerDeleted(CourseWork.Models.AutoDealership.Customer item);
        partial void OnAfterCustomerDeleted(CourseWork.Models.AutoDealership.Customer item);

        public async Task<CourseWork.Models.AutoDealership.Customer> DeleteCustomer(int id)
        {
            var itemToDelete = Context.Customers
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .Include(i => i.Leases)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnCustomerDeleted(itemToDelete);


            Context.Customers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCustomerDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportDealershipCarsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/dealershipcars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/dealershipcars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDealershipCarsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/dealershipcars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/dealershipcars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDealershipCarsRead(ref IQueryable<CourseWork.Models.AutoDealership.DealershipCar> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.DealershipCar>> GetDealershipCars(Query query = null)
        {
            var items = Context.DealershipCars.AsQueryable();

            items = items.Include(i => i.Car);
            items = items.Include(i => i.DealershipCarStatus);
            items = items.Include(i => i.AutoDealership);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDealershipCarsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDealershipCarGet(CourseWork.Models.AutoDealership.DealershipCar item);
        partial void OnGetDealershipCarById(ref IQueryable<CourseWork.Models.AutoDealership.DealershipCar> items);


        public async Task<CourseWork.Models.AutoDealership.DealershipCar> GetDealershipCarById(int id)
        {
            var items = Context.DealershipCars
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Car);
            items = items.Include(i => i.DealershipCarStatus);
            items = items.Include(i => i.AutoDealership);

            OnGetDealershipCarById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDealershipCarGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDealershipCarCreated(CourseWork.Models.AutoDealership.DealershipCar item);
        partial void OnAfterDealershipCarCreated(CourseWork.Models.AutoDealership.DealershipCar item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCar> CreateDealershipCar(CourseWork.Models.AutoDealership.DealershipCar dealershipcar)
        {
            OnDealershipCarCreated(dealershipcar);

            try
            {
                dealershipcar.Id = Context.Employees.Max(c => c.Id) + 1;
                dealershipcar.CreateDate = DateTime.Now;
                dealershipcar.UpdateDate = DateTime.Now;
                Context.DealershipCars.Add(dealershipcar);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(dealershipcar).State = EntityState.Detached;
                throw;
            }

            OnAfterDealershipCarCreated(dealershipcar);

            return dealershipcar;
        }

        public async Task<CourseWork.Models.AutoDealership.DealershipCar> CancelDealershipCarChanges(CourseWork.Models.AutoDealership.DealershipCar item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDealershipCarUpdated(CourseWork.Models.AutoDealership.DealershipCar item);
        partial void OnAfterDealershipCarUpdated(CourseWork.Models.AutoDealership.DealershipCar item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCar> UpdateDealershipCar(int id, CourseWork.Models.AutoDealership.DealershipCar dealershipcar)
        {
            OnDealershipCarUpdated(dealershipcar);

            var itemToUpdate = Context.DealershipCars
                .FirstOrDefault(i => i.Id == dealershipcar.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(dealershipcar);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDealershipCarUpdated(dealershipcar);

            return dealershipcar;
        }

        partial void OnDealershipCarDeleted(CourseWork.Models.AutoDealership.DealershipCar item);
        partial void OnAfterDealershipCarDeleted(CourseWork.Models.AutoDealership.DealershipCar item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCar> DeleteDealershipCar(int id)
        {
            var itemToDelete = Context.DealershipCars
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .Include(i => i.Leases)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDealershipCarDeleted(itemToDelete);


            Context.DealershipCars.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDealershipCarDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportDealershipCarStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/dealershipcarstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/dealershipcarstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDealershipCarStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/dealershipcarstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/dealershipcarstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDealershipCarStatusesRead(ref IQueryable<CourseWork.Models.AutoDealership.DealershipCarStatus> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.DealershipCarStatus>> GetDealershipCarStatuses(Query query = null)
        {
            var items = Context.DealershipCarStatuses.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDealershipCarStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDealershipCarStatusGet(CourseWork.Models.AutoDealership.DealershipCarStatus item);
        partial void OnGetDealershipCarStatusById(ref IQueryable<CourseWork.Models.AutoDealership.DealershipCarStatus> items);


        public async Task<CourseWork.Models.AutoDealership.DealershipCarStatus> GetDealershipCarStatusById(int id)
        {
            var items = Context.DealershipCarStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetDealershipCarStatusById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDealershipCarStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDealershipCarStatusCreated(CourseWork.Models.AutoDealership.DealershipCarStatus item);
        partial void OnAfterDealershipCarStatusCreated(CourseWork.Models.AutoDealership.DealershipCarStatus item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCarStatus> CreateDealershipCarStatus(CourseWork.Models.AutoDealership.DealershipCarStatus dealershipcarstatus)
        {
            OnDealershipCarStatusCreated(dealershipcarstatus);

            var existingItem = Context.DealershipCarStatuses
                .FirstOrDefault(i => i.Id == dealershipcarstatus.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.DealershipCarStatuses.Add(dealershipcarstatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(dealershipcarstatus).State = EntityState.Detached;
                throw;
            }

            OnAfterDealershipCarStatusCreated(dealershipcarstatus);

            return dealershipcarstatus;
        }

        public async Task<CourseWork.Models.AutoDealership.DealershipCarStatus> CancelDealershipCarStatusChanges(CourseWork.Models.AutoDealership.DealershipCarStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDealershipCarStatusUpdated(CourseWork.Models.AutoDealership.DealershipCarStatus item);
        partial void OnAfterDealershipCarStatusUpdated(CourseWork.Models.AutoDealership.DealershipCarStatus item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCarStatus> UpdateDealershipCarStatus(int id, CourseWork.Models.AutoDealership.DealershipCarStatus dealershipcarstatus)
        {
            OnDealershipCarStatusUpdated(dealershipcarstatus);

            var itemToUpdate = Context.DealershipCarStatuses
                .FirstOrDefault(i => i.Id == dealershipcarstatus.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(dealershipcarstatus);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDealershipCarStatusUpdated(dealershipcarstatus);

            return dealershipcarstatus;
        }

        partial void OnDealershipCarStatusDeleted(CourseWork.Models.AutoDealership.DealershipCarStatus item);
        partial void OnAfterDealershipCarStatusDeleted(CourseWork.Models.AutoDealership.DealershipCarStatus item);

        public async Task<CourseWork.Models.AutoDealership.DealershipCarStatus> DeleteDealershipCarStatus(int id)
        {
            var itemToDelete = Context.DealershipCarStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.DealershipCars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDealershipCarStatusDeleted(itemToDelete);


            Context.DealershipCarStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDealershipCarStatusDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportDistributorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/distributors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/distributors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDistributorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/distributors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/distributors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDistributorsRead(ref IQueryable<CourseWork.Models.AutoDealership.Distributor> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Distributor>> GetDistributors(Query query = null)
        {
            var items = Context.Distributors.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDistributorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDistributorGet(CourseWork.Models.AutoDealership.Distributor item);
        partial void OnGetDistributorById(ref IQueryable<CourseWork.Models.AutoDealership.Distributor> items);


        public async Task<CourseWork.Models.AutoDealership.Distributor> GetDistributorById(int id)
        {
            var items = Context.Distributors
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetDistributorById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDistributorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDistributorCreated(CourseWork.Models.AutoDealership.Distributor item);
        partial void OnAfterDistributorCreated(CourseWork.Models.AutoDealership.Distributor item);

        public async Task<CourseWork.Models.AutoDealership.Distributor> CreateDistributor(CourseWork.Models.AutoDealership.Distributor distributor)
        {
            OnDistributorCreated(distributor);

            var existingItem = Context.Distributors
                .FirstOrDefault(i => i.Id == distributor.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Distributors.Add(distributor);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(distributor).State = EntityState.Detached;
                throw;
            }

            OnAfterDistributorCreated(distributor);

            return distributor;
        }

        public async Task<CourseWork.Models.AutoDealership.Distributor> CancelDistributorChanges(CourseWork.Models.AutoDealership.Distributor item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDistributorUpdated(CourseWork.Models.AutoDealership.Distributor item);
        partial void OnAfterDistributorUpdated(CourseWork.Models.AutoDealership.Distributor item);

        public async Task<CourseWork.Models.AutoDealership.Distributor> UpdateDistributor(int id, CourseWork.Models.AutoDealership.Distributor distributor)
        {
            OnDistributorUpdated(distributor);

            var itemToUpdate = Context.Distributors
                .FirstOrDefault(i => i.Id == distributor.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(distributor);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDistributorUpdated(distributor);

            return distributor;
        }

        partial void OnDistributorDeleted(CourseWork.Models.AutoDealership.Distributor item);
        partial void OnAfterDistributorDeleted(CourseWork.Models.AutoDealership.Distributor item);

        public async Task<CourseWork.Models.AutoDealership.Distributor> DeleteDistributor(int id)
        {
            var itemToDelete = Context.Distributors
                              .Where(i => i.Id == id)
                              .Include(i => i.CarDeliveries)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnDistributorDeleted(itemToDelete);


            Context.Distributors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDistributorDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeesRead(ref IQueryable<CourseWork.Models.AutoDealership.Employee> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Employee>> GetEmployees(Query query = null)
        {
            var items = Context.Employees.AsQueryable();

            items = items.Include(i => i.AutoDealership);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeeGet(CourseWork.Models.AutoDealership.Employee item);
        partial void OnGetEmployeeById(ref IQueryable<CourseWork.Models.AutoDealership.Employee> items);


        public async Task<CourseWork.Models.AutoDealership.Employee> GetEmployeeById(int id)
        {
            var items = Context.Employees
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AutoDealership);

            OnGetEmployeeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmployeeCreated(CourseWork.Models.AutoDealership.Employee item);
        partial void OnAfterEmployeeCreated(CourseWork.Models.AutoDealership.Employee item);

        public async Task<CourseWork.Models.AutoDealership.Employee> CreateEmployee(CourseWork.Models.AutoDealership.Employee employee)
        {
            OnEmployeeCreated(employee);

            var existingItem = Context.Employees
                .FirstOrDefault(i => i.Id == employee.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                employee.Id = Context.Employees.Max(c => c.Id) + 1;
                employee.CreateDate=DateTime.Now;
                employee.UpdateDate=DateTime.Now;
                Context.Employees.Add(employee);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(employee).State = EntityState.Detached;
                throw;
            }

            OnAfterEmployeeCreated(employee);

            return employee;
        }

        public async Task<CourseWork.Models.AutoDealership.Employee> CancelEmployeeChanges(CourseWork.Models.AutoDealership.Employee item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmployeeUpdated(CourseWork.Models.AutoDealership.Employee item);
        partial void OnAfterEmployeeUpdated(CourseWork.Models.AutoDealership.Employee item);

        public async Task<CourseWork.Models.AutoDealership.Employee> UpdateEmployee(int id, CourseWork.Models.AutoDealership.Employee employee)
        {
            OnEmployeeUpdated(employee);

            var itemToUpdate = Context.Employees
                .FirstOrDefault(i => i.Id == employee.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employee);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmployeeUpdated(employee);

            return employee;
        }

        partial void OnEmployeeDeleted(CourseWork.Models.AutoDealership.Employee item);
        partial void OnAfterEmployeeDeleted(CourseWork.Models.AutoDealership.Employee item);

        public async Task<CourseWork.Models.AutoDealership.Employee> DeleteEmployee(int id)
        {
            var itemToDelete = Context.Employees
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .Include(i => i.Leases)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnEmployeeDeleted(itemToDelete);


            Context.Employees.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportEnginesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/engines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/engines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEnginesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/engines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/engines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEnginesRead(ref IQueryable<CourseWork.Models.AutoDealership.Engine> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Engine>> GetEngines(Query query = null)
        {
            var items = Context.Engines.AsQueryable();

            items = items.Include(i => i.Brand);
            items = items.Include(i => i.EngineType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEnginesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEngineGet(CourseWork.Models.AutoDealership.Engine item);
        partial void OnGetEngineById(ref IQueryable<CourseWork.Models.AutoDealership.Engine> items);


        public async Task<CourseWork.Models.AutoDealership.Engine> GetEngineById(int id)
        {
            var items = Context.Engines
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Brand);
            items = items.Include(i => i.EngineType);

            OnGetEngineById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEngineGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEngineCreated(CourseWork.Models.AutoDealership.Engine item);
        partial void OnAfterEngineCreated(CourseWork.Models.AutoDealership.Engine item);

        public async Task<CourseWork.Models.AutoDealership.Engine> CreateEngine(CourseWork.Models.AutoDealership.Engine engine)
        {
            OnEngineCreated(engine);

            var existingItem = Context.Engines
                .FirstOrDefault(i => i.Id == engine.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.Engines.Add(engine);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(engine).State = EntityState.Detached;
                throw;
            }

            OnAfterEngineCreated(engine);

            return engine;
        }

        public async Task<CourseWork.Models.AutoDealership.Engine> CancelEngineChanges(CourseWork.Models.AutoDealership.Engine item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEngineUpdated(CourseWork.Models.AutoDealership.Engine item);
        partial void OnAfterEngineUpdated(CourseWork.Models.AutoDealership.Engine item);

        public async Task<CourseWork.Models.AutoDealership.Engine> UpdateEngine(int id, CourseWork.Models.AutoDealership.Engine engine)
        {
            OnEngineUpdated(engine);

            var itemToUpdate = Context.Engines
                .FirstOrDefault(i => i.Id == engine.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(engine);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEngineUpdated(engine);

            return engine;
        }

        partial void OnEngineDeleted(CourseWork.Models.AutoDealership.Engine item);
        partial void OnAfterEngineDeleted(CourseWork.Models.AutoDealership.Engine item);

        public async Task<CourseWork.Models.AutoDealership.Engine> DeleteEngine(int id)
        {
            var itemToDelete = Context.Engines
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnEngineDeleted(itemToDelete);


            Context.Engines.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEngineDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportEngineTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/enginetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/enginetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEngineTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/enginetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/enginetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEngineTypesRead(ref IQueryable<CourseWork.Models.AutoDealership.EngineType> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.EngineType>> GetEngineTypes(Query query = null)
        {
            var items = Context.EngineTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEngineTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEngineTypeGet(CourseWork.Models.AutoDealership.EngineType item);
        partial void OnGetEngineTypeById(ref IQueryable<CourseWork.Models.AutoDealership.EngineType> items);


        public async Task<CourseWork.Models.AutoDealership.EngineType> GetEngineTypeById(int id)
        {
            var items = Context.EngineTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetEngineTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEngineTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEngineTypeCreated(CourseWork.Models.AutoDealership.EngineType item);
        partial void OnAfterEngineTypeCreated(CourseWork.Models.AutoDealership.EngineType item);

        public async Task<CourseWork.Models.AutoDealership.EngineType> CreateEngineType(CourseWork.Models.AutoDealership.EngineType enginetype)
        {
            OnEngineTypeCreated(enginetype);

            var existingItem = Context.EngineTypes
                .FirstOrDefault(i => i.Id == enginetype.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.EngineTypes.Add(enginetype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(enginetype).State = EntityState.Detached;
                throw;
            }

            OnAfterEngineTypeCreated(enginetype);

            return enginetype;
        }

        public async Task<CourseWork.Models.AutoDealership.EngineType> CancelEngineTypeChanges(CourseWork.Models.AutoDealership.EngineType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEngineTypeUpdated(CourseWork.Models.AutoDealership.EngineType item);
        partial void OnAfterEngineTypeUpdated(CourseWork.Models.AutoDealership.EngineType item);

        public async Task<CourseWork.Models.AutoDealership.EngineType> UpdateEngineType(int id, CourseWork.Models.AutoDealership.EngineType enginetype)
        {
            OnEngineTypeUpdated(enginetype);

            var itemToUpdate = Context.EngineTypes
                .FirstOrDefault(i => i.Id == enginetype.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(enginetype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEngineTypeUpdated(enginetype);

            return enginetype;
        }

        partial void OnEngineTypeDeleted(CourseWork.Models.AutoDealership.EngineType item);
        partial void OnAfterEngineTypeDeleted(CourseWork.Models.AutoDealership.EngineType item);

        public async Task<CourseWork.Models.AutoDealership.EngineType> DeleteEngineType(int id)
        {
            var itemToDelete = Context.EngineTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Engines)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnEngineTypeDeleted(itemToDelete);


            Context.EngineTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEngineTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportGearBoxTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/gearboxtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/gearboxtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGearBoxTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/gearboxtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/gearboxtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGearBoxTypesRead(ref IQueryable<CourseWork.Models.AutoDealership.GearBoxType> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.GearBoxType>> GetGearBoxTypes(Query query = null)
        {
            var items = Context.GearBoxTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnGearBoxTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGearBoxTypeGet(CourseWork.Models.AutoDealership.GearBoxType item);
        partial void OnGetGearBoxTypeById(ref IQueryable<CourseWork.Models.AutoDealership.GearBoxType> items);


        public async Task<CourseWork.Models.AutoDealership.GearBoxType> GetGearBoxTypeById(int id)
        {
            var items = Context.GearBoxTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetGearBoxTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGearBoxTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGearBoxTypeCreated(CourseWork.Models.AutoDealership.GearBoxType item);
        partial void OnAfterGearBoxTypeCreated(CourseWork.Models.AutoDealership.GearBoxType item);

        public async Task<CourseWork.Models.AutoDealership.GearBoxType> CreateGearBoxType(CourseWork.Models.AutoDealership.GearBoxType gearboxtype)
        {
            OnGearBoxTypeCreated(gearboxtype);

            var existingItem = Context.GearBoxTypes
                .FirstOrDefault(i => i.Id == gearboxtype.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.GearBoxTypes.Add(gearboxtype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(gearboxtype).State = EntityState.Detached;
                throw;
            }

            OnAfterGearBoxTypeCreated(gearboxtype);

            return gearboxtype;
        }

        public async Task<CourseWork.Models.AutoDealership.GearBoxType> CancelGearBoxTypeChanges(CourseWork.Models.AutoDealership.GearBoxType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGearBoxTypeUpdated(CourseWork.Models.AutoDealership.GearBoxType item);
        partial void OnAfterGearBoxTypeUpdated(CourseWork.Models.AutoDealership.GearBoxType item);

        public async Task<CourseWork.Models.AutoDealership.GearBoxType> UpdateGearBoxType(int id, CourseWork.Models.AutoDealership.GearBoxType gearboxtype)
        {
            OnGearBoxTypeUpdated(gearboxtype);

            var itemToUpdate = Context.GearBoxTypes
                .FirstOrDefault(i => i.Id == gearboxtype.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(gearboxtype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGearBoxTypeUpdated(gearboxtype);

            return gearboxtype;
        }

        partial void OnGearBoxTypeDeleted(CourseWork.Models.AutoDealership.GearBoxType item);
        partial void OnAfterGearBoxTypeDeleted(CourseWork.Models.AutoDealership.GearBoxType item);

        public async Task<CourseWork.Models.AutoDealership.GearBoxType> DeleteGearBoxType(int id)
        {
            var itemToDelete = Context.GearBoxTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnGearBoxTypeDeleted(itemToDelete);


            Context.GearBoxTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGearBoxTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportLeaseProposalConditionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leaseproposalconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leaseproposalconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeaseProposalConditionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leaseproposalconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leaseproposalconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeaseProposalConditionsRead(ref IQueryable<CourseWork.Models.AutoDealership.LeaseProposalCondition> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.LeaseProposalCondition>> GetLeaseProposalConditions(Query query = null)
        {
            var items = Context.LeaseProposalConditions.AsQueryable();

            items = items.Include(i => i.Condition);
            items = items.Include(i => i.LeaseProposal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeaseProposalConditionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeaseProposalConditionGet(CourseWork.Models.AutoDealership.LeaseProposalCondition item);
        partial void OnGetLeaseProposalConditionById(ref IQueryable<CourseWork.Models.AutoDealership.LeaseProposalCondition> items);


        public async Task<CourseWork.Models.AutoDealership.LeaseProposalCondition> GetLeaseProposalConditionById(int id)
        {
            var items = Context.LeaseProposalConditions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Condition);
            items = items.Include(i => i.LeaseProposal);

            OnGetLeaseProposalConditionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeaseProposalConditionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeaseProposalConditionCreated(CourseWork.Models.AutoDealership.LeaseProposalCondition item);
        partial void OnAfterLeaseProposalConditionCreated(CourseWork.Models.AutoDealership.LeaseProposalCondition item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposalCondition> CreateLeaseProposalCondition(CourseWork.Models.AutoDealership.LeaseProposalCondition leaseproposalcondition)
        {
            OnLeaseProposalConditionCreated(leaseproposalcondition);

            var existingItem = Context.LeaseProposalConditions
                .FirstOrDefault(i => i.Id == leaseproposalcondition.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.LeaseProposalConditions.Add(leaseproposalcondition);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(leaseproposalcondition).State = EntityState.Detached;
                throw;
            }

            OnAfterLeaseProposalConditionCreated(leaseproposalcondition);

            return leaseproposalcondition;
        }

        public async Task<CourseWork.Models.AutoDealership.LeaseProposalCondition> CancelLeaseProposalConditionChanges(CourseWork.Models.AutoDealership.LeaseProposalCondition item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeaseProposalConditionUpdated(CourseWork.Models.AutoDealership.LeaseProposalCondition item);
        partial void OnAfterLeaseProposalConditionUpdated(CourseWork.Models.AutoDealership.LeaseProposalCondition item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposalCondition> UpdateLeaseProposalCondition(int id, CourseWork.Models.AutoDealership.LeaseProposalCondition leaseproposalcondition)
        {
            OnLeaseProposalConditionUpdated(leaseproposalcondition);

            var itemToUpdate = Context.LeaseProposalConditions
                .FirstOrDefault(i => i.Id == leaseproposalcondition.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(leaseproposalcondition);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeaseProposalConditionUpdated(leaseproposalcondition);

            return leaseproposalcondition;
        }

        partial void OnLeaseProposalConditionDeleted(CourseWork.Models.AutoDealership.LeaseProposalCondition item);
        partial void OnAfterLeaseProposalConditionDeleted(CourseWork.Models.AutoDealership.LeaseProposalCondition item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposalCondition> DeleteLeaseProposalCondition(int id)
        {
            var itemToDelete = Context.LeaseProposalConditions
                .FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLeaseProposalConditionDeleted(itemToDelete);


            Context.LeaseProposalConditions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeaseProposalConditionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportLeaseProposalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leaseproposals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leaseproposals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeaseProposalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leaseproposals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leaseproposals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeaseProposalsRead(ref IQueryable<CourseWork.Models.AutoDealership.LeaseProposal> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.LeaseProposal>> GetLeaseProposals(Query query = null)
        {
            var items = Context.LeaseProposals.AsQueryable();

            items = items.Include(i => i.LeaseType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeaseProposalsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeaseProposalGet(CourseWork.Models.AutoDealership.LeaseProposal item);
        partial void OnGetLeaseProposalById(ref IQueryable<CourseWork.Models.AutoDealership.LeaseProposal> items);


        public async Task<CourseWork.Models.AutoDealership.LeaseProposal> GetLeaseProposalById(int id)
        {
            var items = Context.LeaseProposals
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.LeaseType);

            OnGetLeaseProposalById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeaseProposalGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeaseProposalCreated(CourseWork.Models.AutoDealership.LeaseProposal item);
        partial void OnAfterLeaseProposalCreated(CourseWork.Models.AutoDealership.LeaseProposal item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposal> CreateLeaseProposal(CourseWork.Models.AutoDealership.LeaseProposal leaseproposal)
        {
            OnLeaseProposalCreated(leaseproposal);

            try
            {
                leaseproposal.Id = Context.LeaseProposals.Max(c => c.Id) + 1;
                leaseproposal.CreateDate = DateTime.Now;
                leaseproposal.UpdateDate = DateTime.Now;
                Context.LeaseProposals.Add(leaseproposal);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(leaseproposal).State = EntityState.Detached;
                throw;
            }

            OnAfterLeaseProposalCreated(leaseproposal);

            return leaseproposal;
        }

        public async Task<CourseWork.Models.AutoDealership.LeaseProposal> CancelLeaseProposalChanges(CourseWork.Models.AutoDealership.LeaseProposal item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeaseProposalUpdated(CourseWork.Models.AutoDealership.LeaseProposal item);
        partial void OnAfterLeaseProposalUpdated(CourseWork.Models.AutoDealership.LeaseProposal item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposal> UpdateLeaseProposal(int id, CourseWork.Models.AutoDealership.LeaseProposal leaseproposal)
        {
            OnLeaseProposalUpdated(leaseproposal);

            var itemToUpdate = Context.LeaseProposals
                .FirstOrDefault(i => i.Id == leaseproposal.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(leaseproposal);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeaseProposalUpdated(leaseproposal);

            return leaseproposal;
        }

        partial void OnLeaseProposalDeleted(CourseWork.Models.AutoDealership.LeaseProposal item);
        partial void OnAfterLeaseProposalDeleted(CourseWork.Models.AutoDealership.LeaseProposal item);

        public async Task<CourseWork.Models.AutoDealership.LeaseProposal> DeleteLeaseProposal(int id)
        {
            var itemToDelete = Context.LeaseProposals
                              .Where(i => i.Id == id)
                              .Include(i => i.LeaseProposalConditions)
                              .Include(i => i.Leases)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLeaseProposalDeleted(itemToDelete);


            Context.LeaseProposals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeaseProposalDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportLeasesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeasesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeasesRead(ref IQueryable<CourseWork.Models.AutoDealership.Lease> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.Lease>> GetLeases(Query query = null)
        {
            var items = Context.Leases.AsQueryable();

            items = items.Include(i => i.Customer);
            items = items.Include(i => i.DealershipCar);
            items = items.Include(i => i.Employee);
            items = items.Include(i => i.LeaseProposal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeasesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeaseGet(CourseWork.Models.AutoDealership.Lease item);
        partial void OnGetLeaseById(ref IQueryable<CourseWork.Models.AutoDealership.Lease> items);


        public async Task<CourseWork.Models.AutoDealership.Lease> GetLeaseById(int id)
        {
            var items = Context.Leases
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Customer);
            items = items.Include(i => i.DealershipCar);
            items = items.Include(i => i.Employee);
            items = items.Include(i => i.LeaseProposal);

            OnGetLeaseById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeaseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeaseCreated(CourseWork.Models.AutoDealership.Lease item);
        partial void OnAfterLeaseCreated(CourseWork.Models.AutoDealership.Lease item);

        public async Task<CourseWork.Models.AutoDealership.Lease> CreateLease(CourseWork.Models.AutoDealership.Lease lease)
        {
            OnLeaseCreated(lease);

            try
            {
                lease.CreateDate = DateTime.Now;
                lease.UpdateDate = DateTime.Now;
                Context.Leases.Add(lease);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(lease).State = EntityState.Detached;
                throw;
            }

            OnAfterLeaseCreated(lease);

            return lease;
        }

        public async Task<CourseWork.Models.AutoDealership.Lease> CancelLeaseChanges(CourseWork.Models.AutoDealership.Lease item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeaseUpdated(CourseWork.Models.AutoDealership.Lease item);
        partial void OnAfterLeaseUpdated(CourseWork.Models.AutoDealership.Lease item);

        public async Task<CourseWork.Models.AutoDealership.Lease> UpdateLease(int id, CourseWork.Models.AutoDealership.Lease lease)
        {
            OnLeaseUpdated(lease);

            var itemToUpdate = Context.Leases
                .FirstOrDefault(i => i.Id == lease.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(lease);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeaseUpdated(lease);

            return lease;
        }

        partial void OnLeaseDeleted(CourseWork.Models.AutoDealership.Lease item);
        partial void OnAfterLeaseDeleted(CourseWork.Models.AutoDealership.Lease item);

        public async Task<CourseWork.Models.AutoDealership.Lease> DeleteLease(int id)
        {
            var itemToDelete = Context.Leases
                .FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLeaseDeleted(itemToDelete);


            Context.Leases.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeaseDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportLeaseTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leasetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leasetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeaseTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/leasetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/leasetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeaseTypesRead(ref IQueryable<CourseWork.Models.AutoDealership.LeaseType> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.LeaseType>> GetLeaseTypes(Query query = null)
        {
            var items = Context.LeaseTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeaseTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeaseTypeGet(CourseWork.Models.AutoDealership.LeaseType item);
        partial void OnGetLeaseTypeById(ref IQueryable<CourseWork.Models.AutoDealership.LeaseType> items);


        public async Task<CourseWork.Models.AutoDealership.LeaseType> GetLeaseTypeById(int id)
        {
            var items = Context.LeaseTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetLeaseTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeaseTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeaseTypeCreated(CourseWork.Models.AutoDealership.LeaseType item);
        partial void OnAfterLeaseTypeCreated(CourseWork.Models.AutoDealership.LeaseType item);

        public async Task<CourseWork.Models.AutoDealership.LeaseType> CreateLeaseType(CourseWork.Models.AutoDealership.LeaseType leasetype)
        {
            OnLeaseTypeCreated(leasetype);

            var existingItem = Context.LeaseTypes
                .FirstOrDefault(i => i.Id == leasetype.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.LeaseTypes.Add(leasetype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(leasetype).State = EntityState.Detached;
                throw;
            }

            OnAfterLeaseTypeCreated(leasetype);

            return leasetype;
        }

        public async Task<CourseWork.Models.AutoDealership.LeaseType> CancelLeaseTypeChanges(CourseWork.Models.AutoDealership.LeaseType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeaseTypeUpdated(CourseWork.Models.AutoDealership.LeaseType item);
        partial void OnAfterLeaseTypeUpdated(CourseWork.Models.AutoDealership.LeaseType item);

        public async Task<CourseWork.Models.AutoDealership.LeaseType> UpdateLeaseType(int id, CourseWork.Models.AutoDealership.LeaseType leasetype)
        {
            OnLeaseTypeUpdated(leasetype);

            var itemToUpdate = Context.LeaseTypes
                .FirstOrDefault(i => i.Id == leasetype.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(leasetype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeaseTypeUpdated(leasetype);

            return leasetype;
        }

        partial void OnLeaseTypeDeleted(CourseWork.Models.AutoDealership.LeaseType item);
        partial void OnAfterLeaseTypeDeleted(CourseWork.Models.AutoDealership.LeaseType item);

        public async Task<CourseWork.Models.AutoDealership.LeaseType> DeleteLeaseType(int id)
        {
            var itemToDelete = Context.LeaseTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.LeaseProposals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnLeaseTypeDeleted(itemToDelete);


            Context.LeaseTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeaseTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportMultimediaOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/multimediaoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/multimediaoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMultimediaOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/multimediaoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/multimediaoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMultimediaOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.MultimediaOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.MultimediaOption>> GetMultimediaOptions(Query query = null)
        {
            var items = Context.MultimediaOptions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMultimediaOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMultimediaOptionGet(CourseWork.Models.AutoDealership.MultimediaOption item);
        partial void OnGetMultimediaOptionById(ref IQueryable<CourseWork.Models.AutoDealership.MultimediaOption> items);


        public async Task<CourseWork.Models.AutoDealership.MultimediaOption> GetMultimediaOptionById(int id)
        {
            var items = Context.MultimediaOptions
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetMultimediaOptionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMultimediaOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMultimediaOptionCreated(CourseWork.Models.AutoDealership.MultimediaOption item);
        partial void OnAfterMultimediaOptionCreated(CourseWork.Models.AutoDealership.MultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.MultimediaOption> CreateMultimediaOption(CourseWork.Models.AutoDealership.MultimediaOption multimediaoption)
        {
            OnMultimediaOptionCreated(multimediaoption);

            var existingItem = Context.MultimediaOptions
                .FirstOrDefault(i => i.Id == multimediaoption.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.MultimediaOptions.Add(multimediaoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(multimediaoption).State = EntityState.Detached;
                throw;
            }

            OnAfterMultimediaOptionCreated(multimediaoption);

            return multimediaoption;
        }

        public async Task<CourseWork.Models.AutoDealership.MultimediaOption> CancelMultimediaOptionChanges(CourseWork.Models.AutoDealership.MultimediaOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMultimediaOptionUpdated(CourseWork.Models.AutoDealership.MultimediaOption item);
        partial void OnAfterMultimediaOptionUpdated(CourseWork.Models.AutoDealership.MultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.MultimediaOption> UpdateMultimediaOption(int id, CourseWork.Models.AutoDealership.MultimediaOption multimediaoption)
        {
            OnMultimediaOptionUpdated(multimediaoption);

            var itemToUpdate = Context.MultimediaOptions
                .FirstOrDefault(i => i.Id == multimediaoption.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(multimediaoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMultimediaOptionUpdated(multimediaoption);

            return multimediaoption;
        }

        partial void OnMultimediaOptionDeleted(CourseWork.Models.AutoDealership.MultimediaOption item);
        partial void OnAfterMultimediaOptionDeleted(CourseWork.Models.AutoDealership.MultimediaOption item);

        public async Task<CourseWork.Models.AutoDealership.MultimediaOption> DeleteMultimediaOption(int id)
        {
            var itemToDelete = Context.MultimediaOptions
                              .Where(i => i.Id == id)
                              .Include(i => i.CarMultimediaOptions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnMultimediaOptionDeleted(itemToDelete);


            Context.MultimediaOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMultimediaOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportPaymentMethodsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/paymentmethods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/paymentmethods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPaymentMethodsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/paymentmethods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/paymentmethods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPaymentMethodsRead(ref IQueryable<CourseWork.Models.AutoDealership.PaymentMethod> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.PaymentMethod>> GetPaymentMethods(Query query = null)
        {
            var items = Context.PaymentMethods.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPaymentMethodsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPaymentMethodGet(CourseWork.Models.AutoDealership.PaymentMethod item);
        partial void OnGetPaymentMethodById(ref IQueryable<CourseWork.Models.AutoDealership.PaymentMethod> items);


        public async Task<CourseWork.Models.AutoDealership.PaymentMethod> GetPaymentMethodById(int id)
        {
            var items = Context.PaymentMethods
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetPaymentMethodById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPaymentMethodGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPaymentMethodCreated(CourseWork.Models.AutoDealership.PaymentMethod item);
        partial void OnAfterPaymentMethodCreated(CourseWork.Models.AutoDealership.PaymentMethod item);

        public async Task<CourseWork.Models.AutoDealership.PaymentMethod> CreatePaymentMethod(CourseWork.Models.AutoDealership.PaymentMethod paymentmethod)
        {
            OnPaymentMethodCreated(paymentmethod);

            var existingItem = Context.PaymentMethods
                .FirstOrDefault(i => i.Id == paymentmethod.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.PaymentMethods.Add(paymentmethod);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(paymentmethod).State = EntityState.Detached;
                throw;
            }

            OnAfterPaymentMethodCreated(paymentmethod);

            return paymentmethod;
        }

        public async Task<CourseWork.Models.AutoDealership.PaymentMethod> CancelPaymentMethodChanges(CourseWork.Models.AutoDealership.PaymentMethod item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPaymentMethodUpdated(CourseWork.Models.AutoDealership.PaymentMethod item);
        partial void OnAfterPaymentMethodUpdated(CourseWork.Models.AutoDealership.PaymentMethod item);

        public async Task<CourseWork.Models.AutoDealership.PaymentMethod> UpdatePaymentMethod(int id, CourseWork.Models.AutoDealership.PaymentMethod paymentmethod)
        {
            OnPaymentMethodUpdated(paymentmethod);

            var itemToUpdate = Context.PaymentMethods
                .FirstOrDefault(i => i.Id == paymentmethod.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(paymentmethod);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPaymentMethodUpdated(paymentmethod);

            return paymentmethod;
        }

        partial void OnPaymentMethodDeleted(CourseWork.Models.AutoDealership.PaymentMethod item);
        partial void OnAfterPaymentMethodDeleted(CourseWork.Models.AutoDealership.PaymentMethod item);

        public async Task<CourseWork.Models.AutoDealership.PaymentMethod> DeletePaymentMethod(int id)
        {
            var itemToDelete = Context.PaymentMethods
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnPaymentMethodDeleted(itemToDelete);


            Context.PaymentMethods.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPaymentMethodDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportSafetyOptionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/safetyoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/safetyoptions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSafetyOptionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/safetyoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/safetyoptions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSafetyOptionsRead(ref IQueryable<CourseWork.Models.AutoDealership.SafetyOption> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.SafetyOption>> GetSafetyOptions(Query query = null)
        {
            var items = Context.SafetyOptions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSafetyOptionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSafetyOptionGet(CourseWork.Models.AutoDealership.SafetyOption item);
        partial void OnGetSafetyOptionById(ref IQueryable<CourseWork.Models.AutoDealership.SafetyOption> items);


        public async Task<CourseWork.Models.AutoDealership.SafetyOption> GetSafetyOptionById(int id)
        {
            var items = Context.SafetyOptions
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetSafetyOptionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSafetyOptionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSafetyOptionCreated(CourseWork.Models.AutoDealership.SafetyOption item);
        partial void OnAfterSafetyOptionCreated(CourseWork.Models.AutoDealership.SafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.SafetyOption> CreateSafetyOption(CourseWork.Models.AutoDealership.SafetyOption safetyoption)
        {
            OnSafetyOptionCreated(safetyoption);

            var existingItem = Context.SafetyOptions
                .FirstOrDefault(i => i.Id == safetyoption.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.SafetyOptions.Add(safetyoption);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(safetyoption).State = EntityState.Detached;
                throw;
            }

            OnAfterSafetyOptionCreated(safetyoption);

            return safetyoption;
        }

        public async Task<CourseWork.Models.AutoDealership.SafetyOption> CancelSafetyOptionChanges(CourseWork.Models.AutoDealership.SafetyOption item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSafetyOptionUpdated(CourseWork.Models.AutoDealership.SafetyOption item);
        partial void OnAfterSafetyOptionUpdated(CourseWork.Models.AutoDealership.SafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.SafetyOption> UpdateSafetyOption(int id, CourseWork.Models.AutoDealership.SafetyOption safetyoption)
        {
            OnSafetyOptionUpdated(safetyoption);

            var itemToUpdate = Context.SafetyOptions
                .FirstOrDefault(i => i.Id == safetyoption.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(safetyoption);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSafetyOptionUpdated(safetyoption);

            return safetyoption;
        }

        partial void OnSafetyOptionDeleted(CourseWork.Models.AutoDealership.SafetyOption item);
        partial void OnAfterSafetyOptionDeleted(CourseWork.Models.AutoDealership.SafetyOption item);

        public async Task<CourseWork.Models.AutoDealership.SafetyOption> DeleteSafetyOption(int id)
        {
            var itemToDelete = Context.SafetyOptions
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSafetyOptions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnSafetyOptionDeleted(itemToDelete);


            Context.SafetyOptions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSafetyOptionDeleted(itemToDelete);

            return itemToDelete;
        }

        public async Task ExportSaleStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/salestatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/salestatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSaleStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealership/salestatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealership/salestatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSaleStatusesRead(ref IQueryable<CourseWork.Models.AutoDealership.SaleStatus> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealership.SaleStatus>> GetSaleStatuses(Query query = null)
        {
            var items = Context.SaleStatuses.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSaleStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSaleStatusGet(CourseWork.Models.AutoDealership.SaleStatus item);
        partial void OnGetSaleStatusById(ref IQueryable<CourseWork.Models.AutoDealership.SaleStatus> items);


        public async Task<CourseWork.Models.AutoDealership.SaleStatus> GetSaleStatusById(int id)
        {
            var items = Context.SaleStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);


            OnGetSaleStatusById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSaleStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSaleStatusCreated(CourseWork.Models.AutoDealership.SaleStatus item);
        partial void OnAfterSaleStatusCreated(CourseWork.Models.AutoDealership.SaleStatus item);

        public async Task<CourseWork.Models.AutoDealership.SaleStatus> CreateSaleStatus(CourseWork.Models.AutoDealership.SaleStatus salestatus)
        {
            OnSaleStatusCreated(salestatus);

            var existingItem = Context.SaleStatuses
                .FirstOrDefault(i => i.Id == salestatus.Id);

            if (existingItem != null)
            {
                throw new Exception("Item already available");
            }

            try
            {
                Context.SaleStatuses.Add(salestatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salestatus).State = EntityState.Detached;
                throw;
            }

            OnAfterSaleStatusCreated(salestatus);

            return salestatus;
        }

        public async Task<CourseWork.Models.AutoDealership.SaleStatus> CancelSaleStatusChanges(CourseWork.Models.AutoDealership.SaleStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
                entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
                entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSaleStatusUpdated(CourseWork.Models.AutoDealership.SaleStatus item);
        partial void OnAfterSaleStatusUpdated(CourseWork.Models.AutoDealership.SaleStatus item);

        public async Task<CourseWork.Models.AutoDealership.SaleStatus> UpdateSaleStatus(int id, CourseWork.Models.AutoDealership.SaleStatus salestatus)
        {
            OnSaleStatusUpdated(salestatus);

            var itemToUpdate = Context.SaleStatuses
                .FirstOrDefault(i => i.Id == salestatus.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salestatus);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSaleStatusUpdated(salestatus);

            return salestatus;
        }

        partial void OnSaleStatusDeleted(CourseWork.Models.AutoDealership.SaleStatus item);
        partial void OnAfterSaleStatusDeleted(CourseWork.Models.AutoDealership.SaleStatus item);

        public async Task<CourseWork.Models.AutoDealership.SaleStatus> DeleteSaleStatus(int id)
        {
            var itemToDelete = Context.SaleStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
                throw new Exception("Item no longer available");
            }

            OnSaleStatusDeleted(itemToDelete);


            Context.SaleStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSaleStatusDeleted(itemToDelete);

            return itemToDelete;
        }
    }
}