<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditApartment.aspx.cs" Inherits="Admin.EditApartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-5" style="display:flex; flex-direction:column;">
            <div class="row p-5">
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" for="txtName" Text="Naziv apartmana"></asp:label>
                    <asp:TextBox class="form-control" ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="reqName" ControlToValidate="txtName" ErrorMessage="Molim unesite naziv apartmana" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" for="txtEngName" Text="Engleski naziv apartmana"></asp:label>
                    <asp:TextBox CssClass="form-control" ID="txtEngName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEngName" ErrorMessage="Molim unesite engleski naziv apartmana" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Adresa apartmana" for="txtAddress"></asp:label>
                    <asp:TextBox CssClass="form-control" ID="txtAddress" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAddress" ErrorMessage="Molim unesite adresu apartmana" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Udaljenost od obale" for="txtBeachDistance"></asp:label>
                    <asp:TextBox CssClass="form-control" type="number" ID="txtBeachDistance" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtBeachDistance" ErrorMessage="Molim unesite udaljenost od obale" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Broj odraslih mjesta" for="txtAdults"></asp:label>
                    <asp:TextBox CssClass="form-control" type="number" ID="txtAdults" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAdults" ErrorMessage="Molim unesite broj odraslih mjesta" />

                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Broj dječjih mjesta" for="txtChildren"></asp:label>
                    <asp:TextBox CssClass="form-control" type="number" ID="txtChildren" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtChildren" ErrorMessage="Molim unesite broj dječjih mjesta" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Cijena apartmana" for="validationDefaultxtPricet02"></asp:label>
                    <asp:TextBox CssClass="form-control" type="number" ID="txtPrice" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtPrice" ErrorMessage="Molim unesite cijenu apartmana" />
                </div>
                <div class="col-md-4 mb-3">
                    <asp:label runat="server" CssClass="form-label" Text="Broj soba u apartmanu" for="txtRoomsCount"></asp:label>
                    <asp:TextBox CssClass="form-control" type="number" ID="txtRoomsCount" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtRoomsCount" ErrorMessage="Molim unesite broj soba u apartmanu" />
                </div>
            </div>
            <div class="row p-5">
                <div class="col">
                    <asp:Label ID="lblCity" CssClass="form-label" for="ddlCity" meta:resourcekey="lblDdlCity" runat="server" Text="Grad"></asp:Label>
                    <asp:DropDownList ID="ddlCity" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col">
                    <asp:Label ID="Label1" CssClass="form-label" for="ddlStatus" meta:resourcekey="lblDdlCity" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col">
                    <asp:Label ID="Label2" CssClass="form-label" for="ddlOwner" meta:resourcekey="lblDdlCity" runat="server" Text="Vlasnik"></asp:Label>
                    <asp:DropDownList ID="ddlOwner" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col">
                    <asp:Label ID="Label3" CssClass="form-label" for="ddlTags" meta:resourcekey="lblDdlTag" runat="server" Text="Oznake"></asp:Label>
                    <asp:DropDownList AutoPostBack="false" ID="ddlTags" CssClass="form-select" runat="server"></asp:DropDownList>
                    <asp:Button style="margin-top: 10px;"  ID="btnAddTag" CssClass="btn btn-primary" OnClick="btnAddTag_Click" runat="server" Text="Dodaj Tag" CausesValidation="false" />
                </div>
            </div>
            <div class="container p-4">
                 <asp:Repeater ID="rptTags" runat="server">
             <HeaderTemplate>
                        <table class="table">
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
                                <asp:LinkButton CommandArgument='<%# Eval("Name") %>' CausesValidation="false" ID="btnRemove" OnClick="btnRemove_Click" runat="server">Ukloni</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
        </asp:Repeater>
            </div>
            <div class="row p-5">
                <div class="btn-group" style="width:50%; align-self:center; margin:0 auto;">
                    <asp:Button runat="server" Text="Spremi" ID="btnSpremi" OnClick="btnSpremi_Click" CssClass="btn btn-primary"  />
                    <asp:Button ID="btnOdustani" OnClick="btnOdustani_Click" CssClass="btn btn-primary " runat="server" Text="Odustani" CausesValidation="False"/>
                </div>
            </div>
        </div>
</asp:Content>