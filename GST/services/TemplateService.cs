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
    public class TemplateService : iCRUD<template>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<TemplateDatatable> GetList(TemplateSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<TemplateDatatable>("template_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Add(template templateData)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var obj = ctx.template.FirstOrDefault(f => f.Id == templateData.Id);
                    if (obj != null)
                    {
                        ctx.Entry(obj).CurrentValues.SetValues(templateData);
                    }
                    else
                    {
                        ctx.template.Add(templateData);
                    }
                    return ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Get(object id)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public template Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.template.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

    }
}

