<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMsg.aspx.cs" Inherits="Web.ShowMsg" %>
<%@ Import Namespace="System.Security.Policy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div>  
        <table width="490" height="325" border="0" align="center" cellpadding="0" cellspacing="0" background="Images/showinfo.png">
            <tr>
                <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="50">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="40">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="50">&nbsp;</td>
                        <td style="text-align: center">
               
                            <%=Msg%>

                        </td>
                        <td width="40">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="50">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="40">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="50" class="style1">&nbsp;</td>
                        <td style="text-align: center">
                            5秒钟以后自动跳转到 <a href="<%=Url%>"> <%=Title %></a>
                        </td>
                        <td width="40">&nbsp;</td>
                    </tr>
                </table></td>
            </tr>
        </table>
    </div>
</form>

</body>
</html>
