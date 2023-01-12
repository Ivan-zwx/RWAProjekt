<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditApartmentTags.aspx.cs" Inherits="Admin.EditApartmentTags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="container p-2" style="display: flex; flex-direction: column;">
        <div class="row p-5">
            <div class="col md-5">
                <asp:Label runat="server" CssClass="form-label" for="txtName" Text="Naziv apartmana"></asp:Label>
                <asp:TextBox class="form-control" ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col md-5">
                <asp:Label ID="Label3" CssClass="form-label" for="ddlTags" meta:resourcekey="lblDdlTag" runat="server" Text="Tagovi"></asp:Label>
                <asp:DropDownList AutoPostBack="false" ID="ddlTags" CssClass="form-select" runat="server"></asp:DropDownList>
                <asp:Button Style="margin-top: 10px;" ID="btnAddTag" CssClass="btn btn-outline-primary" OnClick="btnAddTag_Click" runat="server" Text="Dodaj tag" CausesValidation="false" />
            </div>
        </div>
    </div>

    <div class="container p-4">
        <asp:Repeater ID="rptTags" runat="server">
            <HeaderTemplate>
                <table id="myTable" class="table table-striped">
                    <thead>
                        <tr>
                            <th scope="col">#</th>
                            <th scope="col">Naziv</th>
                            <th scope="col">Kreirano</th>
                            <th scope="col">Tip</th>
                            <th hidden="hidden"></th>
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
                        <asp:Button CommandArgument='<%# Eval("Name") %>' CausesValidation="false" ID="btnRemove" OnClick="btnRemove_Click" CssClass="btn btn-outline-danger" Text="Ukloni" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                    </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div class="row p-2">
        <div class="btn-group gap-2" style="width: 30%; align-self: center; margin: 0 auto;">
            <asp:Button runat="server" Text="Spremi" ID="btnSpremi" OnClick="btnSpremi_Click" CssClass="btn btn-outline-primary" />
            <asp:Button ID="btnOdustani" OnClick="btnOdustani_Click" CssClass="btn btn-outline-danger " runat="server" Text="Odustani" CausesValidation="False" />
        </div>
    </div>

</asp:Content>
