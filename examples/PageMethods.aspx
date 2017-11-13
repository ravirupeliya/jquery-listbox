<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageMethods.aspx.cs" Inherits="TestCalendarControl.PageMethods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<script type="text/javascript">
    function InsertLabelData() {        
        PageMethods.GetLabelText("Dipak","Jid",onSuccess, onFailure);
    }
 
    function onSuccess(result) {
        alert(result);
    }
 
    function onFailure(error) {
        alert(error);
    }

    InsertLabelData();
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptMgr" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>

        <input type="Button" 
                        onclick="InsertLabelData()" 
                        value="Read" />
    </div>
    </form>
</body>
</html>
