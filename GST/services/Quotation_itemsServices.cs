﻿using GST.Controllers;
using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class Quotation_itemsServices : iCRUD<quotation_items>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<Quotation_itemsDatatable> GetQuot_itemsList(Quotation_itemsSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<Quotation_itemsDatatable>("quotation_items_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int AddItems(List<quotation_items> quotation_itemsData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    int? QuotationID = quotation_itemsData.FirstOrDefault().QuotationID;
                    if (QuotationID != null)
                    {
                        List<quotation_items> quoteItems = ctx.quotation_items.Where(w => w.QuotationID == QuotationID).ToList();
                        ctx.quotation_items.RemoveRange(quoteItems);
                    }
                    ctx.quotation_items.AddRange(quotation_itemsData);
                    ctx.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.quotation_items.FirstOrDefault(f => f.Id == Id);
                db.quotation_items.Remove(data);
                var result = db.SaveChanges();
                return result;
            }
        }


        public List<quotation_items> Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.quotation_items.Where(f => f.QuotationID == Id).ToList();
                return data;
            }
        }

        quotation_items iCRUD<quotation_items>.Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.quotation_items.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public int Add(quotation_items obj)
        {
            using (var db = new AppDb())
            {
                db.quotation_items.Add(obj);
                var data = db.SaveChanges();
                return data;
            }
        }
    }
}
