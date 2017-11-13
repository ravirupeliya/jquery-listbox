using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestCalendarControl
{
    public partial class CalendarControl : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillddlDayOfWeek();             
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {            

            LoadCalendar(cdrFromDate.SelectedDate,cdrToDate.SelectedDate); 

            /*
            //outCalendar.Visible = true;
            string month = "Jan";// should be in the format of Jan, Feb, Mar, Apr, etc...
            int yearofMonth = Convert.ToInt32("2016");
            DateTime dateTime = Convert.ToDateTime("01-" + month + "-" + yearofMonth);
            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("Monday");
            dt.Columns.Add("Tuesday");
            dt.Columns.Add("Wednesday");
            dt.Columns.Add("Thursday");
            dt.Columns.Add("Friday");
            dt.Columns.Add("Saturday");
            dt.Columns.Add("Sunday");
            dr = dt.NewRow();
            for (int i = 0; i < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i += 1)
            {        
                if (Convert.ToDateTime(dateTime.AddDays(i)).DayOfWeek == DayOfWeek.Monday)
                {
                    dr["Monday"] = i + 1;
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
                {
                    dr["Tuesday"] = i + 1;
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Wednesday)
                {
                    dr["Wednesday"] = i + 1;
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
                {
                    dr["Thursday"] = i + 1;
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Friday)
                {
                    dr["Friday"] = i + 1;                    
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    dr["Saturday"] = i + 1;                    
                }
                if (dateTime.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    dr["Sunday"] = i + 1;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    continue;
                }
                if (i == DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - 1)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }

            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
             */ 
        }

        protected void grdMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drWeek = e.Row.DataItem as DataRowView;
                CultureInfo provider = CultureInfo.InvariantCulture;

                for (int i = 0; i <= 6; i++)
                {
                    if (drWeek[i] != null && !string.IsNullOrEmpty(drWeek[i].ToString()))
                    {
                        DateTime objDateTime = DateTime.ParseExact(drWeek[i].ToString(),"dd-MM-yyyy",provider); //Convert.ToDateTime(drWeek[i].ToString());

                        if (objDateTime >= cdrFromDate.SelectedDate && objDateTime <= cdrToDate.SelectedDate)
                        {                            
                            e.Row.Cells[i].Text = objDateTime.Day.ToString();
                        }
                        else
                        {
                            e.Row.Cells[i].Text = string.Empty;
                            e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.Height, "20px");
                            continue;
                        }

                        if (Convert.ToInt16(objDateTime.DayOfWeek) == Convert.ToInt16(ddlDayOfWeek.SelectedValue) &&
                            (
                                Convert.ToInt16(ddlWeekOfMonth.SelectedValue) == -1 ||
                                Convert.ToInt16(ddlWeekOfMonth.SelectedValue) == e.Row.RowIndex
                            ) &&                            
                            (
                                Convert.ToInt16(ddlMonthOfYear.SelectedValue) == -1 ||
                                CheckMonthInterval(objDateTime)
                            ))
                        {
                            e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.BackgroundColor,"yellow");
                        }
                    }
                }            
            }
        }

        #endregion

        #region Methods

        private void FillddlDayOfWeek()
        {
            ddlDayOfWeek.Items.Add(new ListItem("None","-1"));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Sunday.ToString(), DayOfWeek.Sunday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Monday.ToString(), DayOfWeek.Monday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Tuesday.ToString(), DayOfWeek.Tuesday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Wednesday.ToString(), DayOfWeek.Wednesday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Thursday.ToString(), DayOfWeek.Thursday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Friday.ToString(), DayOfWeek.Friday.GetHashCode().ToString()));
            ddlDayOfWeek.Items.Add(new ListItem(DayOfWeek.Saturday.ToString(), DayOfWeek.Saturday.GetHashCode().ToString()));
        }

        private void LoadCalendar(DateTime objStartDate,DateTime objEndDate)
        {
            List<DataTable> lstMonths = new List<DataTable>();

            while (objStartDate <= objEndDate)
            {
                DataTable dtMonth = GetMonth(objStartDate.Year,objStartDate.Month);
                lstMonths.Add(dtMonth);

                GridView grdMonth = new GridView();
                grdMonth.RowDataBound += grdMonth_RowDataBound;
                grdMonth.DataSource = dtMonth;
                grdMonth.DataBind();

                pnlCalender.Controls.Add(grdMonth);

                objStartDate = objStartDate.AddMonths(1);
            }

        }
        
        private DataTable GetMonth(int year, int month)
        {
            DataTable dtMonth = new DataTable();
            DataRow dr;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            dtMonth.Columns.Add("Sunday");
            dtMonth.Columns.Add("Monday");
            dtMonth.Columns.Add("Tuesday");
            dtMonth.Columns.Add("Wednesday");
            dtMonth.Columns.Add("Thursday");
            dtMonth.Columns.Add("Friday");
            dtMonth.Columns.Add("Saturday");            
            dr = dtMonth.NewRow();

            for (int day = 1; day <= daysInMonth; day++)
            { 
                DateTime objDateTime = new DateTime(year,month,day);

                dr[(int)objDateTime.DayOfWeek] = objDateTime.ToString("dd-MM-yyyy");

                if (objDateTime.DayOfWeek == DayOfWeek.Saturday || day == daysInMonth)
                {
                    dtMonth.Rows.Add(dr);
                    dr = dtMonth.NewRow();                    
                }
            }

            return dtMonth;

        }

        private bool NthDayOfMonth(DateTime date, DayOfWeek dow, int n)
        {
            int d = date.Day;
            return date.DayOfWeek == dow && (d - 1) / 7 == (n - 1);
        }       

        private bool CheckMonthInterval(DateTime currentDate)
        {
            if (ddlMonthOfYear.SelectedValue != "-1")
            {
                DateTime dtFromDate = cdrFromDate.SelectedDate;
                DateTime dtToDate = cdrToDate.SelectedDate;
                List<string> monthIntervalList = new List<string>();
                while (dtFromDate <= dtToDate)
                {
                    monthIntervalList.Add(dtFromDate.Month + "-" + dtFromDate.Year);
                    dtFromDate = dtFromDate.AddMonths(Convert.ToInt32(ddlMonthOfYear.SelectedValue));
                }
                return monthIntervalList.Contains(currentDate.Month + "-" + currentDate.Year);                
            }
            else
                return false;
        }





        #endregion

    }
}
