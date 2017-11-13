<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondTest.aspx.cs" Inherits="TestCalendarControl.SecondTest" %>

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
                    <td>
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Calendar ID="cdrFromDate" runat="server"></asp:Calendar>
                    </td>
                    <td>
                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Calendar ID="cdrToDate" runat="server"></asp:Calendar>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDayOfWeek" runat="server" Text="Day Of Week"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDayOfWeek" runat="server">
                                        <asp:ListItem Text="Sunday" Value="0" />
                                        <asp:ListItem Text="Monday" Value="1" />
                                        <asp:ListItem Text="Tuesday" Value="2" />
                                        <asp:ListItem Text="Wednesday" Value="3" />
                                        <asp:ListItem Text="Thursday" Value="4" />
                                        <asp:ListItem Text="Friday" Value="5" />
                                        <asp:ListItem Text="Saturday" Value="6" />
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblWeekOfMonth" runat="server" Text="Week Of Month"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlWeekOfMonth" runat="server">
                                        <asp:ListItem Text="All" Value="-1" />
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                        <asp:ListItem Text="6" Value="6" />
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblMonthInterval" runat="server" Text="Month Interval"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonthInterval" runat="server">
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
                            <tr>
                                <td>
                                    <asp:Label ID="lblDayLabel" runat="server" Text="Day Interval" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDayInterval" runat="server">
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
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblErrorMsg" runat="server" Visible="false" />
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblResult" runat="server" Visible="false" />
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnlCalendar" runat="server">
                        </asp:Panel>
                    </td>

                </tr>
            </table>
        </div>
    </form>
</body>
</html>
