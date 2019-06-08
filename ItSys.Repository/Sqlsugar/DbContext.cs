using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using ItSys.Entity;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ItSys.Repository.Sqlsugar
{
    public class DbContext
    {
        /// <summary>
        /// 是否转换成小写下划线命名
        /// </summary>
        private bool _isConvertLowerUnderLineName = true;
        public SqlSugarClient Db;
        public SimpleClient<SysUser> SysUsers { get { return new SimpleClient<SysUser>(Db); } }
        public DbContext()
        {
            Db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = "Server=localhost;Database=ItSys;User=root;Password=root",
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                    ConfigureExternalServices = new ConfigureExternalServices()
                    {
                        
                        //实体属性配置
                        EntityService = (property, column) =>
                        {
                            var attributes = property.GetCustomAttributes(false);
                            #region 判断实体主键
                            var keyAttribute = attributes.FirstOrDefault(a => a is KeyAttribute);
                            //当属性名称为Id时或实体名称Id时，自动为主键
                            if (property.Name == "Id"
                            || property.Name == $"{property.DeclaringType.Name}Id"
                            || keyAttribute != null)
                            {
                                column.IsPrimarykey = true;
                                column.IsIdentity = true;
                            }
                            #endregion

                            #region 判断实体列名
                            var columnAttribute = attributes.FirstOrDefault(a => a is ColumnAttribute);
                            if (columnAttribute != null)
                            {
                                column.DbColumnName = (columnAttribute as ColumnAttribute).Name;
                            }
                            else if (_isConvertLowerUnderLineName)
                            {
                                column.DbColumnName = _convertLowerUnderLineName(property.Name);
                            }
                            #endregion
                            
                            #region 判断实体属性是否忽略映射
                            var notMappedAttribute = attributes.FirstOrDefault(a => a is NotMappedAttribute);
                            if (notMappedAttribute != null)
                            {
                                column.IsIgnore = true;
                            } 
                            #endregion

                        },
                        EntityNameService = (type, entity) =>
                        {                            
                            var attributes = type.GetCustomAttributes(false);
                            #region 判断实体表名
                            var tableAttribute = attributes.FirstOrDefault(a => a is TableAttribute);
                            if (tableAttribute != null)
                            {
                                entity.DbTableName = (tableAttribute as TableAttribute).Name;
                            }
                            else if (_isConvertLowerUnderLineName)
                            {
                                entity.DbTableName = _convertLowerUnderLineName(type.Name);
                            } 
                            #endregion
                        }
                    }
                });
        }

        /// <summary>
        /// 转换成小写下划线命名
        /// </summary>
        /// <param name="name">原名称</param>
        /// <returns></returns>
        private string _convertLowerUnderLineName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            return Regex.Replace(name, "([^_A-Z])([A-Z])", "$1_$2").ToLower();
        }

        public SimpleClient<T> Set<T>() where T : class, new()
        {
            return new SimpleClient<T>(Db);
        }

    }
}
