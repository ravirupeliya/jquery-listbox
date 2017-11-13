using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace TestCalendarControl
{
    public partial class ThirdTest : System.Web.UI.Page
    {

        #region Variable

        Int32 NthWeekOfMonth = 0;

        List<string> monthIntervalList = null;

        DateTime DateInterval = DateTime.Now;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadCalendar(cdrFromDate.SelectedDate,cdrToDate.SelectedDate);
        }

        protected void gvMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            if(e.Row.RowIndex == 0)
                NthWeekOfMonth = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drWeek = e.Row.DataItem as DataRowView;

                for (int i = 0; i <= 6; i++)
                {
                    if (drWeek[i] != null && Convert.ToString(drWeek[i]) != string.Empty)
                    {
                        DateTime objDateTime = DateTime.ParseExact(drWeek[i].ToString(),"dd-MM-yyyy",provider);

                        if (objDateTime >= cdrFromDate.SelectedDate && objDateTime <= cdrToDate.SelectedDate)
                            e.Row.Cells[i].Text = objDateTime.Day.ToString();
                        else
                        {
                            e.Row.Cells[i].Text = string.Empty;
                            continue;
                        }

                        if((Int32)objDateTime.DayOfWeek == Convert.ToInt32(ddlDayOfWeek.SelectedValue))
                        {
                            NthWeekOfMonth +=1;
                        }

                        if (ddlDayInterval.SelectedValue != "-1")
                        {
                            if (objDateTime == DateInterval)
                            {
                                DateInterval = DateInterval.AddDays(Convert.ToInt32(ddlDayInterval.SelectedValue));
                                e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.BackgroundColor,"yellow");
                            }
                        }
                        else if ((Int32)objDateTime.DayOfWeek == Convert.ToInt32(ddlDayOfWeek.SelectedValue) &&
                            (
                                ddlWeekOfMonth.SelectedValue == "-1" ||
                                Convert.ToInt16(ddlWeekOfMonth.SelectedValue) == NthWeekOfMonth
                            ) &&
                            (
                                ddlMonthInterval.SelectedValue == "-1" ||
                                CheckMonthInterval(objDateTime)
                            ))
                        {
                            e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.BackgroundColor, "yellow");
                        }


                    }
                }
            
            }

        }

        #endregion

        #region Methods

        private void LoadCalendar(DateTime FromDate, DateTime ToDate)
        {

            DateTime dtFromDate = FromDate;
            DateTime dtToDate = ToDate;
            DataTable dtMonth = null;
            PrepareMonthIntervalList(FromDate, ToDate);
            DateInterval = FromDate;

            while (dtFromDate <= dtToDate)
            {
                dtMonth = GetMonth(dtFromDate.Year, dtFromDate.Month);

                GridView gvMonth = new GridView();
                gvMonth.RowDataBound += gvMonth_RowDataBound;
                gvMonth.DataSource = dtMonth;
                gvMonth.DataBind();

                pnlCalendar.Controls.Add(new LiteralControl() { Text = dtFromDate.ToString("MMM, yyyy") });
                pnlCalendar.Controls.Add(new LiteralControl() { Text = "<br/>" });
                pnlCalendar.Controls.Add(gvMonth);
                pnlCalendar.Controls.Add(new LiteralControl() { Text = "<br/>" });

                dtFromDate = dtFromDate.AddMonths(1);
            }
        }
        

        private DataTable GetMonth(Int32 Year, Int32 Month)
        {
            Int32 daysInMonth = DateTime.DaysInMonth(Year, Month);
            DataRow drWeek = null;
            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add(DayOfWeek.Sunday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Monday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Tuesday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Wednesday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Thursday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Friday.ToString());
            dtMonth.Columns.Add(DayOfWeek.Saturday.ToString());
            drWeek = dtMonth.NewRow();

            for (Int32 day = 1; day <= daysInMonth; day++)
            {
                DateTime objDateTime = new DateTime(Year, Month, day);                                
                drWeek[objDateTime.DayOfWeek.ToString()] = objDateTime.ToString("dd-MM-yyyy");

                if (objDateTime.DayOfWeek == DayOfWeek.Saturday || day == daysInMonth)
                {
                    dtMonth.Rows.Add(drWeek);
                    drWeek = dtMonth.NewRow();
                }
            }
             
            return dtMonth;    
        }

        private bool CheckMonthInterval(DateTime objDateTime)
        {
            if (ddlMonthInterval.SelectedValue != "-1")
            {
                return monthIntervalList.Contains(objDateTime.Month + "-" + objDateTime.Year);
            }
            else
                return true;
        }

        private void PrepareMonthIntervalList(DateTime dtFromDate,DateTime dtToDate)
        {
            if (ddlMonthInterval.SelectedValue != "-1")
            {
                monthIntervalList = new List<string>();
                while (dtFromDate <= dtToDate)
                {                    
                    monthIntervalList.Add(dtFromDate.Month.ToString() + "-" + dtFromDate.Year.ToString());
                    dtFromDate = dtFromDate.AddMonths(Convert.ToInt16(ddlMonthInterval.SelectedValue));                    
                }
            }
        }   
    
        #endregion
    }
}