<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="Admin.Apartments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:Panel runat="server" ID="pnlApartments">
        <div class="container" style="display:flex; flex-direction:column; justify-content:center; align-items:flex-start; width:30%">
            <asp:Label ID="lblStatusFilter" CssClass="form-label" runat="server" for="ddlStatusFilter" Text="Filtriranje po statusu"></asp:Label>
            <asp:DropDownList AutoPostBack="true" ID="ddlStatusFilter" CssClass="form-select" runat="server"></asp:DropDownList>
            <asp:Label ID="lblCityFilter" CssClass="form-label" runat="server" for="ddlCityFilter" Text="Filtriranje po gradu"></asp:Label>
            <asp:DropDownList AutoPostBack="true" ID="ddlCityFilter" CssClass="form-select"  runat="server"></asp:DropDownList>
        </div>
        <div class="container">
                <fieldset class="p-4">
                    <legend>Popis apartmana</legend>
                    <asp:Repeater runat="server" ID="Repeater">
                        <HeaderTemplate>
                            <table id="myTable" class="table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Naziv</th>
                                        <th scope="col">Grad</th>
                                        <th scope="col">Adresa</th>
                                        <th scope="col">Odrasli</th>
                                        <th scope="col">Djeca</th>
                                        <th scope="col">Sobe</th>
                                        <th scope="col">Udaljenost od obale</th>
                                        <th scope="col">Cijena</th>
                                        <th hidden="hidden"></th>
                                        <th hidden="hidden"></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("Id") %></th>
                                <th><%# Eval("Name") %></th>
                                <td><%# Eval("City") %></td>
                                <td><%# Eval("Address") %></td>
                                <td><%# Eval("MaxAdults") %></td>
                                <td><%# Eval("MaxChildren") %></td>
                                <td><%# Eval("TotalRooms") %></td>
                                <td><%# Eval("BeachDistance") %></td>
                                <td><%# Eval("Price") %></td>
                                <td> 
                                    <asp:Button OnClick="btnUredi_Click" CssClass="btn btn-outline-secondary" CommandArgument='<%# Eval("Id") %>' Text="Uredi" ID="btnUredi" runat="server" CausesValidation="False"  UseSubmitBehavior="false"/>
                                </td>
                                <td> 
                                    <asp:Button OnClick="btnUrediTagove_Click" CssClass="btn btn-outline-secondary" CommandArgument='<%# Eval("Id") %>' Text="Uredi tagove" ID="btnUrediTagove" runat="server" CausesValidation="False"  UseSubmitBehavior="false"/>
                                </td>
                                <td> 
                                    <asp:Button OnClick="btnDelete_Click" CssClass="btn btn-outline-danger" CommandArgument='<%# Eval("Id") %>' Text="Obrisi" ID="btnDelete" runat="server"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>

                    </asp:Repeater>

                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-primary" runat="server" Text="Dodaj" />
                </fieldset>
            </div>
    </asp:Panel>
</asp:Content>

