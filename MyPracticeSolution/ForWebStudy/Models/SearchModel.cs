using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ForWebStudy.Models
{
    #region 问题查询参数
    public enum SearchTypeOption
    {
        SplitPage = 1,//分页
        Top = 2, //获取前数位记录
        Total = 3 //全部记录
    }

    public enum OrderOption
    {
        Asc = 0,
        Desc
    }
    public enum StatusOption
    {
        [Description("激活")]
        Active = 0,
        [Description("删除")]
        Deleted = 1, //已删除   
        [Description("All")]
        NONE = 2 //All   
    }
    /// <summary>
    /// 查询模板，请不要使用json字符串反序列化得到的实例，可能会因为数据类型不匹配而拿不到预定义的属性
    /// </summary>
    public class SearchQustionModel : DynamicObject
    {
        public SearchQustionModel() : base() { }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public override System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames()
        {
            var members = this.GetType().GetProperties().Select(c => c.Name);
            return members;
        }

        public string SearchKey { get; set; }
        public string TypeID { get; set; }
        private Dictionary<string, OrderOption> _OrderDict;
        public Dictionary<string, OrderOption> OrderDict
        {
            get
            {
                if (_OrderDict == null)
                {
                    _OrderDict = new Dictionary<string, OrderOption>();
                }
                return _OrderDict;
            }
            set
            {
                _OrderDict = value;
            }
        }
        public StatusOption? Status { get; set; }

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public SearchTypeOption? SearchType { get; set; }
    }
    public class DynamicSearchQustionModel:DynamicObject
    {
        public DynamicSearchQustionModel() : base() { }

        public string SearchKey { get; set; }
        public string TypeID { get; set; }
        private Dictionary<string, OrderOption> _OrderDict;
        public Dictionary<string, OrderOption> OrderDict
        {
            get
            {
                if (_OrderDict == null)
                {
                    _OrderDict = new Dictionary<string, OrderOption>();
                }
                return _OrderDict;
            }
            set
            {
                _OrderDict = value;
            }
        }
        public StatusOption? Status { get; set; }

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public SearchTypeOption? SearchType { get; set; }
    }




    #endregion
}