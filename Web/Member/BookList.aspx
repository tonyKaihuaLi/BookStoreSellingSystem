<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="Web.Member.BookList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="../Css/pageBarStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Repeater ID="BookListRepeater" runat="server" OnItemCommand="BookListRepeater_ItemCommand">
        <ItemTemplate>
            <TABLE>
                <TBODY>
                <TR>
                    <TD rowSpan=2><A 
                                      href="<%#Eval("Id","/BookDetail_{0}.aspx") %>"><IMG 
                                            id="ctl00_cphContent_dl_Books_ctl01_imgBook"
                                            style="CURSOR: hand" height=121 
                                            alt="<%#Eval("Title") %>" hspace="4" 
                                            src="<%#Eval("ISBN","/Images/BookCovers/{0}.jpg") %>" width=95></A> 
                    </TD>
                    <TD style="FONT-SIZE: small; COLOR: red" width=650><A 
                        class=booktitle id=link_prd_name 
                        href="<%#GetString(Eval("PublishDate")) %><%#Eval("Id") %>.html" target=_blank 
                        name="link_prd_name"><%#Eval("Title") %></A> 
                    </TD></TR>
                <TR>
                    <TD align=left><SPAN 
                                       style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%#Eval("Author") %></SPAN><BR><BR><SPAN 
                                       style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%#this.CutString(Eval("ContentDescription").ToString(),150)%></SPAN> 
                    </TD></TR>
                <TR>
                    <TD align=right colSpan=2><SPAN 
                                                  style="FONT-WEIGHT: bold; FONT-SIZE: 13px; LINE-HEIGHT: 20px">&yen; 
                        <%#Eval("UnitPrice","{0:0.00}") %></SPAN> </TD></TR></TBODY></TABLE>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>
    <div class="page_nav">
        <%=Common.PageBarHelper.GetPagaBar(PageIndex,PageCount)%>
    </div>
</asp:Content>
