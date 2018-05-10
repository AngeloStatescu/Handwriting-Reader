<%@ Page Title="" Language="C#" MasterPageFile="~/HandwritingReader.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HandwritingReader.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="margin-left: 35%; margin-right: 35%; margin-top: 50px;">
    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3>Account Registration</h3>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6 col-sm-6 col-xs-6 col-lg-6">
                            <label>Username</label>
                            <input id="Username" class="input-sm form-control" type="text" runat="server" />
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-6 col-lg-6">
                            <label>Password</label>
                            <input id="Password" class="input-sm form-control" type="password" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                            <label>Email</label>
                            <input id="Email" class="input-sm form-control" type="text" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                            <asp:Button ID="RegisterButton" runat="server" Text="Register" CssClass="btn btn-info" style="margin: auto; display: block;  width: 40%;" OnClick="RegisterButton_Click"/>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="text-align: center;">
                            <p id="RegisterError" runat="server" style="color: red;"></p>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
</div>

</asp:Content>
