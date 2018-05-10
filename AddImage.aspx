<%@ Page Title="" Language="C#" MasterPageFile="~/HandwritingReader.Master" AutoEventWireup="true" CodeBehind="AddImage.aspx.cs" Inherits="HandwritingReader.AddImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>

        #ContentPlaceHolder1_FileUploadControl {
            float:left;
        }

        #ContentPlaceHolder1_Button1 {
            float:right;
            font-size: 18px;
        }

        #ContentPlaceHolder1_Button2 {
            font-size: 18px;
            width: 30%;
            float: left;
        }

        #ContentPlaceHolder1_Button3 {
            font-size: 18px;
            width: 30%;
            float: right;
        }

        #ContentPlaceHolder1_Panel1 {
            width: 100%;
            height: 500px;
            border: 1px solid white;
            overflow: auto;
        }

        .inactiveButton {
            display: none;
        }

    </style>
    <div style="margin-left: 25%; margin-right: 25%; margin-top: 50px;">
        <div class="row">

            <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1>Add Image</h1>
                    </div>

                    <div class="panel-body">
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px; padding-bottom: 30px;">
                            <asp:FileUpload ID="FileUploadControl" runat="server" />
                            <asp:Button ID="Button1" runat="server" Text="Upload" onclick="UploadButton_Click" CssClass="btn btn-default" />
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px; margin-top: 30px;">
                            <asp:Panel ID="Panel1" runat="server"><asp:Image ID="Image1" runat="server" /></asp:Panel>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="font-size: 20px; margin-top: 30px; text-align: center;">
                            <asp:Button ID="Button2" runat="server" Text="Generate text" CssClass="btn btn-default" OnClick="Button2_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Download result" CssClass="btn btn-default inactiveButton" OnClick="Button3_Click" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
