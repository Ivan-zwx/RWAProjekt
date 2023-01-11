<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Admin.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                        <table id="myTable" class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Naziv</th>
                                    <th scope="col">Kreirano</th>
                                    <th scope="col"></th>
                                    <th scope="col"></th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval("Id") %></th>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("CreatedAt") %></td>
                            <td><%# Eval("Type") %></td>
                            <td>
                                <asp:LinkButton OnClick="LinkButton1_Click" CommandArgument='<%# Eval("Id") %>' ID="LinkButton1" runat="server">Odaberi</asp:LinkButton>
                            </td>
                            
                            <td>
                                <asp:LinkButton CssClass="btn btn-danger" Visible='<%#(int)Eval("NumberOfApartments") == 0 %>' CommandArgument='<%# Eval("Id") %>' OnClick="btnDelete_Click" ID="btnDelete" runat="server">Obrisi</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddTag" OnClick="btnAddTag_Click" CssClass="btn btn-primary" runat="server" Text="Dodaj" />
            </div>
            <div class="col-md-6">
                <asp:GridView ID="gvAparments" AutoGenerateColumns="false" CssClass="table" runat="server">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Naziv apartmana" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
