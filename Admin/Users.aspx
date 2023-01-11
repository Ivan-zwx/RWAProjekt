<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-4">
            <div class="form-group">
                <fieldset class="p-4">
                    <legend>Popis registriranih korisnika</legend>


                    <asp:Repeater runat="server" ID="rptUsers">
                        <HeaderTemplate>
                            <table id="myTable" class="table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Ime</th>
                                        <th scope="col">E-mail</th>
                                        <th scope="col">Adresa</th>
                                        <th scope="col">Kreirano</th>
                                        <th scope="col">Obrisano</th>
                                        <th scope="col">Telefonski broj</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("Id") %></th>
                                <th><%# Eval("UserName") %></th>
                                <td><%# Eval("Email") %></td>
                                <td><%# Eval("Address") %></td>
                                <td><%# Eval("CreatedAt") %></td>
                                <td><%# Eval("DeletedAt") %></td>
                                <td><%# Eval("PhoneNumber") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>

                    </asp:Repeater>

                </fieldset>
            </div>
        </div>
</asp:Content>
