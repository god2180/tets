<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DNFShop.WebForm1" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 193px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    <table class="auto-style5">
                        <tr>
                            <td class="auto-style1">itemName</td>
                            <td>
                                <asp:Label ID="itemNameLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">unitPrice</td>
                            <td>
                                <asp:Label ID="unitPriceLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td class="auto-style1">2stPrice</td>
                            <td>
                                <asp:Label ID="secondPriceLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">averagePrice</td>
                            <td>
                                <asp:Label ID="averagePriceLabel" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
        <table >
            <tr>
                <td class="auto-style1">itemName</td>
                <td class="auto-style1">
        <asp:Label ID="itemMagName" runat="server" Text="마그토늄"></asp:Label>
                </td>
                <td> <asp:HiddenField ID="MagID" runat="server" /></td>
            </tr>
            <tr>
                <td class="auto-style1">unitPrice</td>
                <td class="auto-style1">
        <asp:Label ID="unitPriceMag" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style1">2stPrice</td>
                <td>
                    <asp:Label ID="secondPriceMag" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">averagePrice</td>
                <td class="auto-style1"> <asp:Label ID="averagePriceMag" runat="server" Text="Label"></asp:Label>
                </td>
                <td><asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="save" /></td>
            </tr>
        </table>
            </div>
        <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" OnLoad="Chart1_Load">
            <Series>
                <asp:Series ChartType="Line" Name="Series1" XValueMember="day" YValueMembers="price">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyConnectionString %>" OnSelecting="SqlDataSource1_Selecting" ProviderName="<%$ ConnectionStrings:MyConnectionString.ProviderName %>" SelectCommand="SELECT [price], [day] FROM [item] WHERE ([itemID] = @itemID) ORDER BY [day]">
            <SelectParameters>
                <asp:Parameter DefaultValue="c6a38ab8c7540cfc51ea2b0b8b610fa7" Name="itemID" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
       
    </form>
</body>
</html>
