<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="restaurantemenus.aspx.cs" Inherits="M17_Food4U.admin.restaurantemenus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Menus do Restaurante</h2>
    <asp:GridView OnRowDataBound="dgv_menus_RowDataBound" EmptyDataText="Sem Menus" CssClass="table" ID="dgv_menus" runat="server" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/admin/restaurantes.aspx" runat="server" />


    <div class="modal fade" id="modal-confirm-delete" tabindex="-1" aria-labelledby="ModalBloquear" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalBloquear"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="modal_body">

                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" onserverclick="btn_ativar_ServerClick" id="btn_ativar" class="btn btn-success">Ativar</button>
                    <button type="button" runat="server" onserverclick="btn_desativar_ServerClick" id="btn_desativar" class="btn btn-danger">Desativar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-confirm-stock" tabindex="-1" aria-labelledby="ModalStock" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalStock"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="modal_body2">

                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" onserverclick="btn_AtivarStock_ServerClick" id="btn_AtivarStock" class="btn btn-success">Ativar</button>
                    <button type="button" runat="server" onserverclick="btn_DesativarStock_ServerClick" id="btn_DesativarStock" class="btn btn-danger">Desativar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script type='text/javascript'>
        function btnblockclick(id, estado) {
            document.cookie = "id_menu=" + id;

            $("#ContentPlaceHolder1_btn_desativar").hide()
            $("#ContentPlaceHolder1_btn_ativar").hide()


            if (estado) {
                $("#ContentPlaceHolder1_btn_desativar").show()
                $("#ContentPlaceHolder1_btn_ativar").hide()

                $("#ModalBloquear").html("Desativar Menu")
                $("#modal_body").html("Tem mesmo a certeza que quer desativar o menu?")
            }
            else {
                $("#ContentPlaceHolder1_btn_desativar").hide()
                $("#ContentPlaceHolder1_btn_ativar").show()

                $("#ModalBloquear").html("Ativar Menu")
                $("#modal_body").html("Tem mesmo a certeza que quer ativar o menu?")
            }

            $('#modal-confirm-delete').modal('show')
        }

        function btnstockclick(id, estado) {

            document.cookie = "id_menu=" + id;


            $("#ContentPlaceHolder1_btn_AtivarStock").hide()
            $("#ContentPlaceHolder1_btn_DesativarStock").hide()


            if (estado) {
                $("#ContentPlaceHolder1_btn_DesativarStock").show()
                $("#ContentPlaceHolder1_btn_AtivarStock").hide()

                $("#ModalStock").html("Colocar menu fora de stock")
                $("#modal_body2").html("Tem mesmo a certeza que quer colocar este menu fora de stock?")
            }
            else {
                $("#ContentPlaceHolder1_btn_DesativarStock").hide()
                $("#ContentPlaceHolder1_btn_AtivarStock").show()

                $("#ModalStock").html("Colocar Menu em stock")
                $("#modal_body2").html("Tem mesmo a certeza que quer colocar este menu em stock?")
            }

            $('#modal-confirm-stock').modal('show')
        }
    </script>
</asp:Content>
