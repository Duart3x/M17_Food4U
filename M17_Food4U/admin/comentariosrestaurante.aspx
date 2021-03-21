<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="comentariosrestaurante.aspx.cs" Inherits="M17_Food4U.admin.comentariosrestaurante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Comentários</h2>
    <asp:GridView OnRowDataBound="dgv_comentarios_RowDataBound" runat="server" EmptyDataText="Sem Comentários" CssClass="table" ID="dgv_comentarios" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/admin/restaurantes.aspx" runat="server" />

    <div class="modal fade" id="modal-confirm-deleteperm" tabindex="-1" aria-labelledby="ModalDelete" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalDelete">Tem a certeza que deseja eliminar este comentário?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Ao eliminar este comentário não irá ser possível voltar a reavê-lo.
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" onserverclick="btn_eliminar_ServerClick"  id="btn_eliminar" class="btn btn-danger">Eliminar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function btndeleteclick(id) {
            document.cookie = "id_comentario=" + id;

            $('#modal-confirm-deleteperm').modal('show')
        }
    </script>
</asp:Content>
