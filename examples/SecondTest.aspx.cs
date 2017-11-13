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
    public partial class SecondTest : System.Web.UI.Page
    {
        #region Global Variables

        List<string> monthIntervalList = null;

        Int16 NthWeekOfMonth = 0;

        DateTime DayInterval = DateTime.Now.AddDays(2);

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cdrFromDate.SelectedDate != null && cdrToDate.SelectedDate != null)
            {
                LoadCalendar(cdrFromDate.SelectedDate, cdrToDate.SelectedDate);
            }
            else
            {
                lblErrorMsg.Text = "Please, Select both from and to date.";
            }

        }

        protected void gvMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drWeek = e.Row.DataItem as DataRowView;
                CultureInfo provider = CultureInfo.InvariantCulture;                

                if(e.Row.RowIndex == 0)
                    NthWeekOfMonth = 0;


                for (int i = 0; i <= 6; i++)
                {
                    if (drWeek[i] != null && Convert.ToString(drWeek[i]) != "")
                    {
                        DateTime objDateTime = DateTime.ParseExact(drWeek[i].ToString(), "dd-MM-yyyy", provider);                        

                        if ((int)objDateTime.DayOfWeek == Convert.ToInt16(ddlDayOfWeek.SelectedValue))
                        {
                            NthWeekOfMonth += 1;
                        }

                        if (objDateTime >= cdrFromDate.SelectedDate && objDateTime <= cdrToDate.SelectedDate)
                            e.Row.Cells[i].Text = objDateTime.Day.ToString();
                        else
                        {
                            e.Row.Cells[i].Text = string.Empty;
                            continue;
                        }

                        if (ddlDayInterval.SelectedValue != "-1")
                        {
                            if (objDateTime == DayInterval)
                            {
                                DayInterval = objDateTime.AddDays(Convert.ToInt16(ddlDayInterval.SelectedValue));
                                e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.BackgroundColor, "yellow");
                                Response.Write(objDateTime.ToString("dd-MM-yyyy") + "<br/>");                            
                            }
                        }

                        else if ((int)objDateTime.DayOfWeek == Convert.ToInt16(ddlDayOfWeek.SelectedValue) &&
                            (
                                (ddlWeekOfMonth.SelectedValue == "-1" ||
                                Convert.ToInt16(ddlWeekOfMonth.SelectedValue) == NthWeekOfMonth)                               
                            ) &&
                            (
                                ddlMonthInterval.SelectedValue == "-1" ||
                                CheckMonthInterval(objDateTime)
                            ))
                        {                            
                            e.Row.Cells[i].Style.Add(HtmlTextWriterStyle.BackgroundColor, "yellow");
                            Response.Write(objDateTime.ToString("dd-MM-yyyy") + "<br/>");                            
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Crate calendar between from date and to date.
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        private void LoadCalendar(DateTime FromDate, DateTime ToDate)
        {
            DateTime dtFromDate = FromDate;
            DateTime dtToDate = ToDate;
            DataTable dtMonth = null;
            FillMonthIntervalList(FromDate, ToDate);
            DayInterval = FromDate;

            while (dtFromDate <= dtToDate)
            {
                //Load particular year and month data.
                dtMonth = LoadMonth(dtFromDate.Year, dtFromDate.Month);

                //Assign Month data to gridview.
                GridView gvMonth = new GridView();
                gvMonth.Visible = false;
                gvMonth.RowDataBound += gvMonth_RowDataBound;
                gvMonth.DataSource = dtMonth;
                gvMonth.DataBind();

                //Add gridview to panel.
                //pnlCalendar.Controls.Add(new LiteralControl() { Text = "<br/>" });
                //pnlCalendar.Controls.Add(new LiteralControl() { Text = dtFromDate.ToString("MMMM, yyyy") });
                //pnlCalendar.Controls.Add(new LiteralControl() { Text = "<br/>" });
                pnlCalendar.Controls.Add(gvMonth);


                //Increase 1 month in from date.
                dtFromDate = dtFromDate.AddMonths(1);
            }
        }

        /// <summary>
        /// Return Month DataTable
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        private DataTable LoadMonth(int Year, int Month)
        {
            int daysInMoth = DateTime.DaysInMonth(Year, Month);
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

            for (int day = 1; day <= daysInMoth; day++)
            {
                DateTime objDatetime = new DateTime(Year, Month, day);
                drWeek[objDatetime.DayOfWeek.ToString()] = objDatetime.ToString("dd-MM-yyyy");

                if (objDatetime.DayOfWeek.ToString() == DayOfWeek.Saturday.ToString() || day == daysInMoth)
                {
                    dtMonth.Rows.Add(drWeek);
                    drWeek = dtMonth.NewRow();
                }
            }
            return dtMonth;
        }

        private bool CheckMonthInterval(DateTime CurrentDate)
        {
            if (ddlMonthInterval.SelectedValue != "-1")
            {
                return monthIntervalList.Contains(CurrentDate.Month.ToString() + "-" + CurrentDate.Year.ToString());
            }
            else
                return true;
        }

        private void FillMonthIntervalList(DateTime FromDate, DateTime ToDate)
        {
            DateTime dtFromDate = FromDate;
            DateTime dtToDate = ToDate;

            if (ddlMonthInterval.SelectedValue != "-1")
            {
                monthIntervalList = new List<string>();
                Int16 monthInterval = Convert.ToInt16(ddlMonthInterval.SelectedValue);

                while (dtFromDate <= dtToDate)
                {
                    monthIntervalList.Add(dtFromDate.Month.ToString() + "-" + dtFromDate.Year.ToString());
                    dtFromDate = dtFromDate.AddMonths(monthInterval);
                }
            }
        }


        #endregion

    }
}