<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="EventDetails.aspx.cs" Inherits="Pages_Student_EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel" style=" background-color:#01579B;color:#fff;">
        <div class="panel-heading">
           <a href="Dashboard.aspx" style="position:page;top:120px;left:50px;color:white"><i class="fa fa-arrow-left fa-2x"></i></a>
            <div class="col-md-12 text-center">
                 <h2 style="color:#ffffff;">    Event Details</h2>
            </div>
           
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="container">
                           <div class="list-group" style="color:#4f07c3 !important;"><%-- #01579B--%>
                        
                                <div class="list-group-item list-group-item-heading z-depth-5">
                                    <div class="row ">
                                        <div class="col-md-4">
                                            <div class="panel panel-danger text-center z-depth-3">
                                                <div class="panel-heading">
                                                   <h4> <span class="panel-title strong">
                                                       <asp:Label ID="lblFromDate" runat="server" ></asp:Label> to <asp:Label ID="lblToDate" runat="server" ></asp:Label>
                                                    </span></h4>
                                                </div>
                                                <div class="panel-body text-danger">
                                                    <asp:Image ID="imgEvenPic" Width="200px" Height="150px" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <h2>
                                                <asp:Label ID="lblEventName"  runat="server" Text=""></asp:Label></h2>
                                            <hr />
                                            <p class="msg">
                                                <small><asp:Label ID="lblEventDescription" Font-Size="Larger" runat="server" Text=""></asp:Label></small>
                                            </p>

                                        </div>
                                        <br /><br />
                                        <asp:LinkButton ID="btnApplyNow" CssClass="btn btn-info btn-rounded btn-lg" runat="server"><span class="fa fa-send"></span> &nbsp; Apply Now </asp:LinkButton>

                                    </div>
                                </div>
                            
                              
                    </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

