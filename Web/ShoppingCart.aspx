<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Web.ShoppingCart" %>
<%@ Import Namespace="Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="Css/themes/ui-lightness/jquery-ui-1.8.2.custom.css" rel="stylesheet" />
    <script src="js/jquery-ui-1.8.2.custom.min.js"></script>
    <script type="text/javascript">
        $(function() {
            getTotalPrice();
        });
        function changeBar(operator, cartId, bookId) {
            var count = $("#txtCount" + bookId).val();
            if (operator == "-") {

                count = parseInt(count);
                count--;
                if (count < 1) {
                    alert("");
                    return false;
                }

            } else if (operator == "+") {
                count++;
                if (count > 1000) {
                    alert("");
                    return false;
                }

            } else {
                alert("");
            }
            $.post("/ashx/EditCart.ashx",
                { "count": count,"cartId":cartId },
                function(data) {
                    if (data == "ok") {
                        $("#txtCount" + bookId).val(count);


                    } else {
                        alert("wrong");
                    }
                });
        }

        function getTotalPrice() {
            var totalPrice = 0;
            $(".align_Center:gt(0)").each(function() {
                var price = $(this).find(".price").text();
                var count = $(this).find("input").val();
                totalPrice = totalPrice + (parseFloat(price) * parseInt(count));
            });
            $("#tMoney").text(fmoney(totalPrice,2));
        }

        function removeProductOnShopppingCart (cartId,control){
            if (confirm("sure?")) {
                $.post("/ashx/EditCart.ashx", { "action": "delete", "cartId": cartId }, function(data) {
                    if (data == "ok") {
                        $(control).parent().parent().remove();
                        getTotalPrice();
                    }
                });
            }
        }

        function changeTextOnBlur(cardID, control) {
            var count = $(control).val();
            var reg = /^\d+$/;
            if (reg.test(count)) {

            } else {
                showDialog("商品数量只能是数字");
                $(control).val($("#pCount").val());

            }

        }

        function fmoney(s, n) {
            n = n > 0 && n <= 20 ? n : 2;
            s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";//更改这里n数也可确定要保留的小数位  
            var l = s.split(".")[0].split("").reverse(),
                r = s.split(".")[1];
            t = "";
            for (i = 0; i < l.length; i++) {
                t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
            }
            return t.split("").reverse().join("") + "." + r.substring(0, 2);//保留2位小数  如果要改动 把substring 最后一位数改动就可  
        }

        function showDialog(msg) {
            $("#errorMsg").text(msg);
            $("#showResult").css("display", "block");
            $("#showResult").dialog({
                height: 240,
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0"  width="98%">
        <tr>
            <td colspan="2">
                <img height="27" 
                    src="images/shop-cart-header-blue.gif" width="206" /><img alt="" 
                    src="Images/png-0170.png" /><asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/myorder.aspx">我的订单</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" width="98%">
          
                <table  cellpadding='0' cellspacing='0' width='100%' >
 <tr class='align_Center Thead'>
    <td width='7%' style='height:30px'>图片</td>
    <td>图书名称</td>
    <td width='14%'>单价</td>
    <td width='11%'>购买数量</td>
    <td width='7%'>删除图书</td>
 </tr>

       
      <%foreach(Cart cartModel in CartList){%>   
<!--一行数据的开始 -->
<tr class='align_Center'>
   <td style='padding:5px 0 5px 0;'><img src='/images/bookcovers/<%=cartModel.Book.ISBN%>.jpg' width="40" height="50" border="0" /></td>
   <td class='align_Left'><%=cartModel.Book.Title %></td>
   <td>
<span class='price'><%=cartModel.Book.UnitPrice.ToString("0.00")%></span>
</td>
   <td><a href='#none' title='减一' onclick="changeBar('-',<%=cartModel.Id%>,<%=cartModel.Book.Id %>)" style='margin-right:2px;' ><img src="Images/bag_close.gif" width="9" height="9" border='none' style='display:inline' /></a>
     <input type='text' id='txtCount<%=cartModel.Book.Id%>' name='txtCount4945' maxlength='3' style='width:30px' onKeyDown='if(event.keyCode == 13) event.returnValue = false' value='<%=cartModel.Count %>' onfocus='changeTxtOnFocus(this);' onblur="changeTextOnBlur(<%=cartModel.Id%>,this);" />
     <a href='#none' title='加一' onclick="changeBar('+',<%=cartModel.Id%>,<%=cartModel.Book.Id %>)" style='margin-left:2px;' ><img src='/images/bag_open.gif' width="9" height="9" border='none' style='display:inline' /></a>   </td>
   <td>
   <a href='#none' id='btn_del_1000357315' onclick="removeProductOnShoppingCart(<%=cartModel.Id%>,this)" >
       删除</a></td>
</tr>
<!--一行数据的结束 -->
<%} %>





 <tr>
    <td class='align_Right Tfoot' colspan='5' style='height:30px'>&nbsp;</td>
 </tr>
</table>
</td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;&nbsp;&nbsp; 商品金额总计：<span ID="tMoney" style="font-size:20px;color:red;font-weight:bold"> 
                   >0</span>元</td>
            <td>
                &nbsp;
               <a href="/Member/BookList.aspx"> <img alt="" src="Images/gobuy.jpg" width="103" height="36" border="0" /> </a><a href="OrderConfirm.aspx"><img src="images/balance.gif" 
                     border="0" /></a>
              
            </td>
        </tr>
    </table>
    </div>
    <div id="showResult" style="display:none">
        <span id="errorMsg" style="font-size:20px;color:red"></span>
    </div>
    <input type="hidden" id="pCount" />
    

</asp:Content>
