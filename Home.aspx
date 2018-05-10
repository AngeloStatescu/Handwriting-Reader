<%@ Page Title="" Language="C#" MasterPageFile="~/HandwritingReader.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HandwritingReader.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .home-image:hover {
            background-color: #000;
            border-width: 2px;
        }

        .home-image {
            border: 1px solid white;
            border-radius: 2px;
            margin-top: 25px;
        }

        a {
            text-decoration: none !important;
        }
    </style>
    <div style="margin-left: 5%; margin-right: 5%; margin-top: 50px;">
        <div class="row">

            <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1>How does this work?</h1>
                    </div>

                    <div class="panel-body">
                        <div class="col-md-12 col-sm-12 col-xs-12 col-lg-12" style="text-align: center; font-size: 20px;">
                               <p>It's very simple. You only need your PNG format image ready and we will take care of the rest.</p>
                               <p>Simply access the "Add Image" page and upload the image with the text you want.</p>
                               <a href="AddImage.aspx">
                                    <img class="home-image" src="Resources/ic_image_white_48dp_2x.png" style="width: 200px; height: 200px;"/>
                               </a>
                            <br />
                            <br />
                            <br />
                               <p>Or try out our "Doodle" page. Draw any letters or words and we will generate the text for you.</p>
                               <a href="Doodle.aspx">
                                    <img class="home-image" src="Resources/ic_palette_white_48dp_2x.png" style="width: 200px; height: 200px;"/>
                               </a>
                            <br />
                            <br />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
