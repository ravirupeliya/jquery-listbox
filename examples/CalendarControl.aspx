<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarControl.aspx.cs" Inherits="TestCalendarControl.CalendarControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <%-- From Calendar --%>
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label></td>
                    <td>
                        <asp:Calendar ID="cdrFromDate" runat="server"></asp:Calendar>
                    </td>

                    <%-- To Calendar --%>
                    <td>
                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label></td>
                    <td>
                        <asp:Calendar ID="cdrToDate" runat="server"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlDayOfWeek" runat="server">                                        
                                    </asp:DropDownList></td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="ddlWeekOfMonth" runat="server">                                        
                                        <asp:ListItem Text="All" Value="-1" />
                                        <asp:ListItem Text="1" Value="0" />
                                        <asp:ListItem Text="2" Value="1" />
                                        <asp:ListItem Text="3" Value="2" />
                                        <asp:ListItem Text="4" Value="3" />
                                        <asp:ListItem Text="5" Value="4" />                                        
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonthOfYear" runat="server">                                        
                                        <asp:ListItem Text="None" Value="-1" />
                                        <asp:ListItem Text="1" Value="2" />
                                        <asp:ListItem Text="2" Value="3" />
                                        <asp:ListItem Text="3" Value="4" />
                                        <asp:ListItem Text="4" Value="5" />
                                        <asp:ListItem Text="5" Value="6" />                                       
                                        <asp:ListItem Text="6" Value="7" />                                        
                                        <asp:ListItem Text="7" Value="8" />                                        
                                        <asp:ListItem Text="8" Value="9" />                                        
                                        <asp:ListItem Text="9" Value="10" />                                        
                                        <asp:ListItem Text="10" Value="11" />                                        
                                        <asp:ListItem Text="11" Value="12" />                                        
                                        <asp:ListItem Text="12" Value="13" />                                         
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnlCalender" runat="server"></asp:Panel>
                    </td>

                </tr>

            </table>

        </div>
    </form>
</body>
</html>

