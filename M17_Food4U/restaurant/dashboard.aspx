<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="M17_Food4U.restaurant.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Dashboard Restaurante</h2>
    <div>
        <div class="restaurant_checks">
            <div class="form-check p-0">
                <asp:CheckBox ID="check_espera" OnCheckedChanged="checks_geral_CheckedChanged" AutoPostBack="true" Text="Em espera" runat="server" />
            </div>

            <div class="form-check p-0">
               <asp:CheckBox ID="check_aserpreparados" OnCheckedChanged="checks_geral_CheckedChanged" AutoPostBack="true" Text="A ser preparados" runat="server" />
            </div>

            <div class="form-check p-0">
               <asp:CheckBox ID="check_concluidos" OnCheckedChanged="checks_geral_CheckedChanged" AutoPostBack="true" Text="Concluidos" runat="server" />
            </div>
        </div>
        <div class="restaurant_inputs">
            <div class="restaurant_input">
                <div class="input-group-prepend">
                    <div class="input-group-text">Dia</div>
                </div>
                <asp:TextBox OnTextChanged="txt_datepicker_TextChanged" CssClass="form-control w-100" ID="txt_datepicker" TextMode="Date" runat="server" AutoPostBack="true" ></asp:TextBox>
            </div>
            <div class="restaurant_input">
                <div class="input-group-prepend">
                    <div class="input-group-text">Hora Início</div>
                </div>
                <asp:TextBox OnTextChanged="txt_hour_start_TextChanged" CssClass="form-control w-100" ID="txt_hour_start" TextMode="Time" runat="server" AutoPostBack="true" ></asp:TextBox>
            </div>
            <div class="restaurant_input">
                <div class="input-group-prepend">
                    <div class="input-group-text">Hora Fim</div>
                </div>
                <asp:TextBox OnTextChanged="txt_hour_end_TextChanged" CssClass="form-control w-100" ID="txt_hour_end" TextMode="Time" runat="server" AutoPostBack="true" ></asp:TextBox>
            </div>
        </div>
    </div>
    <main class="mt-5">
        <asp:GridView EmptyDataText="Sem pedidos" CssClass="table" ID="dgv_pedidos" runat="server" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </main>
    
</asp:Content>