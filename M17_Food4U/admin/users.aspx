<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="M17_Food4U.admin.users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Utilizadores</h2>
    <asp:GridView OnRowDataBound="dgv_utilizadores_RowDataBound" runat="server" EmptyDataText="Sem Utilizadores" CssClass="table" ID="dgv_utilizadores" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>


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

    <div class="modal fade" id="modal-confirm-deleteperm" tabindex="-1" aria-labelledby="ModalDelete" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalDelete">Tem a certeza que deseja eliminar este utilizador?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Ao eliminar o utilizador este torna-se indisponível para sempre. Tem mesmo a certeza que deseja continuar?
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" onserverclick="btn_eliminar_ServerClick"  id="btn_eliminar" class="btn btn-danger">Eliminar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>


    <script>
        function btnblockclick(id, estado) {
            document.cookie = "id_utilizador=" + id;

            $("#ContentPlaceHolder1_btn_desativar").hide()
            $("#ContentPlaceHolder1_btn_ativar").hide()


            if (estado) {
                $("#ContentPlaceHolder1_btn_desativar").show()
                $("#ContentPlaceHolder1_btn_ativar").hide()

                $("#ModalBloquear").html("Desativar Utilizador")
                $("#modal_body").html("Tem mesmo a certeza que quer desativar o utilizador?")
            }
            else {
                $("#ContentPlaceHolder1_btn_desativar").hide()
                $("#ContentPlaceHolder1_btn_ativar").show()

                $("#ModalBloquear").html("Ativar Utilizador")
                $("#modal_body").html("Tem mesmo a certeza que quer ativar o utilizador?")
            }

            $('#modal-confirm-delete').modal('show')
        }

        function btndeleteclick(id) {
            document.cookie = "id_utilizador=" + id;

            $('#modal-confirm-deleteperm').modal('show')
        }
    </script>
</asp:Content>
