using System;
using System.Collections.Generic;
using System.Text;

namespace FaxVerification.Records
{
 
    public class UserInfo
    {
        public string token { get; set; }
        public string langPref { get; set; }
        public string locale { get; set; }
        public string dateFormat { get; set; }
        public string dateSeperator { get; set; }
        public string simpleDateFormat { get; set; }
        public string timeFormat { get; set; }
        public string decimalFormat { get; set; }
        public int addressNumber { get; set; }
        public string alphaName { get; set; }
        public string appsRelease { get; set; }
        public string country { get; set; }
        public string username { get; set; }
        public string longUserId { get; set; }
        public string timeZoneOffset { get; set; }
        public string dstRuleKey { get; set; }
        public DstRule dstRule { get; set; }
        public object startEffectiveDate { get; set; }
        public object ruleDescription { get; set; }
        public object endEffectiveDate { get; set; }
    }

    public class DstRule
    {
        public int startDate { get; set; }
        public int endDate { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public int dstSavings { get; set; }
        public int endDay { get; set; }
        public int endDayOfWeek { get; set; }
        public int endMonth { get; set; }
        public int startDay { get; set; }
        public int startMonth { get; set; }
        public int effectiveYear { get; set; }
        public int dstruleOffset { get; set; }
        public int startDayOfWeek { get; set; }
    }

    public class OCRWorkbench
    {
        public string username { get; set; }
        public string environment { get; set; }
        public string role { get; set; }
        public string jasserver { get; set; }
        public UserInfo userInfo { get; set; }
        public bool userAuthorized { get; set; }
        public object version { get; set; }
        public object poStringJSON { get; set; }
        public object altPoStringJSON { get; set; }
        public string aisSessionCookie { get; set; }
        public bool adminAuthorized { get; set; }
        public bool passwordAboutToExpire { get; set; }
        public string envColor { get; set; }
        public string machineName { get; set; }
        public string externalJASURL { get; set; }
    }


    public class Output
    {
        public int id { get; set; }
        public object value { get; set; }
    }

    public class Result
    {
        public List<Output> output { get; set; }
    }

    public class UpdateResponse
    {
        public string name { get; set; }
        public string template { get; set; }
        public bool submitted { get; set; }
        public Result result { get; set; }
    }

    public class GridData
    {
        public int Id { get; set; }
        public string FullGridId { get; set; }
        public Dictionary<string, string> Columns { get; set; }
        public List<Dictionary<string, object>> Rowset { get; set; }
        public SummaryData Summary { get; set; }
    }

    public class SummaryData
    {
        public int Records { get; set; }
        public bool MoreRecords { get; set; }
    }

    public class FsDATABROWSE_V5643001
    {
        public string Title { get; set; }
        public GridData Data { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Warnings { get; set; }
    }

    public class RootObject
    {
        public FsDATABROWSE_V5643001 Fs_DATABROWSE_V5643001 { get; set; }
        public int StackId { get; set; }
        public int StateId { get; set; }
        public string Rid { get; set; }
        public string CurrentApp { get; set; }
        public string TimeStamp { get; set; }
        public List<object> SysErrors { get; set; }
    }


    #region GetVendorNo

    public class GridData1
    {
        public int Id { get; set; }
        public string FullGridId { get; set; }
        public Row Columns { get; set; }
        public List<Row> Rowset { get; set; }
        public Summary Summary { get; set; }
    }

    public class Row
    {
        public string F5643001_Y56EMDT { get; set; }
        public string F5643001_Y56INVDT { get; set; }
        public string F5643002_ITM { get; set; }
        public string F5643002_UNCS { get; set; }
        public string F5643001_S74IVD { get; set; }
        public string F5643001_EXA { get; set; }
        public string F5643001_VINV { get; set; }
        public string F5643002_CITM { get; set; }
        public string F5643001_KCOO { get; set; }
        public string F5643001_VDNM { get; set; }
        public string F5643001_DOCO { get; set; }
        public string F5643001_Y56PSTS { get; set; }
        public string F5643001_DCTO { get; set; }
        public string F5643001_Y56EMSUB { get; set; }
        public string F5643001_Y56VINVA { get; set; }
        public string F5643002_LITM { get; set; }
        public string F5643002_CRCD { get; set; }
        public string F5643001_Y56EMID { get; set; }
        public string F5643001_Y56TAXA { get; set; }
        public string F5643002_AEXP { get; set; }
        public string F5643002_UORG { get; set; }
        public string F5643001_Y56TAMT { get; set; }
        public string F5643001_AN8V { get; set; }
    }

    public class Summary
    {
        public int Records { get; set; }
        public bool MoreRecords { get; set; }
    }

    public class Data
    {
        public GridData1 GridData { get; set; }
    }

    public class FsDataBrowseV550102
    {
        public string Title { get; set; }
        public Data Data { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Warnings { get; set; }
    }

    public class Root
    {
        public FsDataBrowseV550102 Fs_DATABROWSE_V550102 { get; set; }
        public int StackId { get; set; }
        public int StateId { get; set; }
        public string Rid { get; set; }
        public string CurrentApp { get; set; }
        public string TimeStamp { get; set; }
        public List<object> SysErrors { get; set; }
    }
    #endregion 
}
