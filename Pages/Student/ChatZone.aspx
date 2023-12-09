<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Student/Student.master" AutoEventWireup="true" CodeFile="ChatZone.aspx.cs" Inherits="Pages_Student_ChatZone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/StudentJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <link href="../../CSS/CommonCSS/components_fun.css" rel="stylesheet" />
  <%--  <link href="../../CSS/CommonCSS/chat.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" type="text/css" rel="stylesheet" />

    <style>
        .chat
{
    list-style: none;
    margin: 0;
    padding: 0;
}

.chat li
{
    margin-bottom: 10px;
    padding-bottom: 5px;
    border-bottom: 1px dotted #B3A9A9;
}

.chat li.left .chat-body
{
    margin-left: 60px;
}

.chat li.right .chat-body
{
    margin-right: 60px;
}


.chat li .chat-body p
{
    margin: 0;
    color: #777777;
}

.panel .slidedown .glyphicon, .chat .glyphicon
{
    margin-right: 5px;
}

.panel-body
{
    overflow-y: scroll;
    height: 250px;
}

::-webkit-scrollbar-track
{
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    background-color: #F5F5F5;
}

::-webkit-scrollbar
{
    width: 12px;
    background-color: #F5F5F5;
}

::-webkit-scrollbar-thumb
{
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
    background-color: #555;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="jumbotron visible-md visible-sm visible-xs" style="display:none;">

            <ul class="nav nav-justified navbar-fixed-bottom  ">
                <li class="hvr-float-shadow"><a href="#page-wrap" id="A4" class=" ellipsis" style="background-color: #b56969; color: White"><i class="fa fa-home   "></i>
                </a></li>
                <li class="hvr-float-shadow "><a href="#aboutLibrary" style="background-color: #e05038; color: White"><i class="fa fa-info-circle "></i>
                </a></li>
                <li class="hvr-float-shadow "><a href="#librarian" style="background-color: #16174f; color: White" class="ellipsis"><i class="fa fa-user  "></i></a></li>
                <li class="hvr-float-shadow "><a style="background-color: #963019; color: White" href="#Digital"><i class="fa fa-globe  "></i>
                </a></li>
                <li class="hvr-float-shadow "><a href="#search" style="background-color: #e6af4b; color: White"><i class="fa fa-book "></i>
                </a></li>
                <li class="hvr-float-shadow "><a href="#thought" style="background-color: #6534ff; color: White"><i class="fa fa-quote-right "></i>
                </a></li>
                <li class="hvr-float-shadow"><a href="#location" id="A5" style="background-color: #b56969; color: White"><i class="fa fa-map   "></i>
                </a></li>
                 
            </ul>
        </div>
    <div class="jumbotron visible-lg-inline" style="padding-top: -15px; margin-top: -50px;display:none;">

            <ul class="nav nav-justified affix-top" style="vertical-align: middle; font-size: small!important" data-spy="affix" data-offset-top="50">
                <li class="hvr-float-shadow"><a href="#page-wrap" id="A2" class=" ellipsis" style="background-color: #b56969; color: White"><i class="fa fa-home  fa-2x "></i>
                    <br>
                    <span class="visible-lg-inline ">Home</span>    </a></li>
                <li class="hvr-float-shadow "><a href="#aboutLibrary" style="background-color: #e05038; color: White"><i class="fa fa-info-circle fa-2x"></i>
                    <br>
                    <span class="visible-lg-inline ">About library</span> </a></li>
                <li class="hvr-float-shadow "><a href="#librarian" style="background-color: #16174f; color: White" class="ellipsis"><i class="fa fa-user fa-2x "></i>&nbsp;<br>
                    <span class="visible-lg-inline ">About librarian</span> </a></li>
                <li class="hvr-float-shadow "><a style="background-color: #963019; color: White" href="#Digital"><i class="fa fa-globe  fa-2x"></i>
                    <br>
                    <span class="visible-lg-inline ">Digital zone</span> </a></li>
                <li class="hvr-float-shadow "><a href="#search" style="background-color: #e6af4b; color: White"><i class="fa fa-book fa-2x"></i>
                    <br>
                    <span class="visible-lg-inline ">eBooks Search </span></a></li>
                <li class="hvr-float-shadow "><a href="#thought" style="background-color: #6534ff; color: White"><i class="fa fa-quote-right fa-2x"></i>
                    <br>
                    <span class="visible-lg-inline ">Thought of Day</span> </a></li>
                <li class="hvr-float-shadow"><a href="#location" id="A3" style="background-color: #b56969; color: White"><i class="fa fa-map  fa-2x "></i>
                    <br>
                    <span class="visible-lg-inline ">Location</span> </a></li>
                
            </ul>
        </div>

    <div class="container-fluid hidden">

        <div class="messaging">
            <div class="inbox_msg">
                <div class="inbox_people">
                    <div class="headind_srch">
                        <div class="recent_heading">
                            <h4>Recent</h4>
                        </div>
                        <div class="srch_bar">
                            <div class="stylish-input-group">
                                <input type="text" class="search-bar" placeholder="Search">
                                <span class="input-group-addon">
                                    <button type="button"><i class="fa fa-search" aria-hidden="true"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="inbox_chat">
                        <div class="chat_list active_chat">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="chat_list">
                            <div class="chat_people">
                                <div class="chat_img">
                                    <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                                </div>
                                <div class="chat_ib">
                                    <h5>Sunil Rajput <span class="chat_date">Dec 25</span></h5>
                                    <p>
                                        Test, which is a new approach to have all solutions 
                    astrology under one roof.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mesgs">
                    <div class="msg_history">
                        <div class="incoming_msg">
                            <div class="incoming_msg_img">
                                <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                            </div>
                            <div class="received_msg">
                                <div class="received_withd_msg">
                                    <p>
                                        Test which is a new approach to have all
                    solutions
                                    </p>
                                    <span class="time_date">11:01 AM    |    June 9</span>
                                </div>
                            </div>
                        </div>
                        <div class="outgoing_msg">
                            <div class="sent_msg">
                                <p>
                                    Test which is a new approach to have all
                  solutions
                                </p>
                                <span class="time_date">11:01 AM    |    June 9</span>
                            </div>
                        </div>
                        <div class="incoming_msg">
                            <div class="incoming_msg_img">
                                <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                            </div>
                            <div class="received_msg">
                                <div class="received_withd_msg">
                                    <p>Test, which is a new approach to have</p>
                                    <span class="time_date">11:01 AM    |    Yesterday</span>
                                </div>
                            </div>
                        </div>
                        <div class="outgoing_msg">
                            <div class="sent_msg">
                                <p>Apollo University, Delhi, India Test</p>
                                <span class="time_date">11:01 AM    |    Today</span>
                            </div>
                        </div>
                        <div class="incoming_msg">
                            <div class="incoming_msg_img">
                                <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil">
                            </div>
                            <div class="received_msg">
                                <div class="received_withd_msg">
                                    <p>
                                        We work directly with our designers and suppliers,
                    and sell direct to you, which means quality, exclusive
                    products, at a price anyone can afford.
                                    </p>
                                    <span class="time_date">11:01 AM    |    Today</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="type_msg">
                        <div class="input_msg_write">
                            <input type="text" class="write_msg" placeholder="Type a message" />
                            <button class="msg_send_btn" type="button"><i class="fa fa-paper-plane-o" aria-hidden="true"></i></button>
                        </div>
                    </div>
                </div>
            </div>


            <p class="text-center top_spac">Handcrafted by : <a target="_blank" href="#">Deshpande Foundation  </a></p>

        </div>
    </div>

    <div class="container">
    <div class="row">
        <div class="col-md-5">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-comment"></span> Chat
                    <div class="btn-group pull-right">
                        <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                            <span class="glyphicon glyphicon-chevron-down"></span>
                        </button>
                        <ul class="dropdown-menu slidedown">
                            <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-refresh">
                            </span>Refresh</a></li>
                            <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-ok-sign">
                            </span>Available</a></li>
                            <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-remove">
                            </span>Busy</a></li>
                            <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-time"></span>
                                Away</a></li>
                            <li class="divider"></li>
                            <li><a href="http://www.jquery2dotnet.com"><span class="glyphicon glyphicon-off"></span>
                                Sign Out</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <ul class="chat">
                        <li class="left clearfix"><span class="chat-img pull-left">
                            <img src="http://placehold.it/50/55C1E7/fff&amp;text=U" alt="User Avatar" class="img-circle">
                        </span>
                            <div class="chat-body clearfix">
                                <div class="header">
                                    <strong class="primary-font">Jack Sparrow</strong> <small class="pull-right text-muted">
                                        <span class="glyphicon glyphicon-time"></span>12 mins ago</small>
                                </div>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img pull-right">
                            <img src="http://placehold.it/50/FA6F57/fff&amp;text=ME" alt="User Avatar" class="img-circle">
                        </span>
                            <div class="chat-body clearfix">
                                <div class="header">
                                    <small class=" text-muted"><span class="glyphicon glyphicon-time"></span>13 mins ago</small>
                                    <strong class="pull-right primary-font">Bhaumik Patel</strong>
                                </div>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="left clearfix"><span class="chat-img pull-left">
                            <img src="http://placehold.it/50/55C1E7/fff&amp;text=U" alt="User Avatar" class="img-circle">
                        </span>
                            <div class="chat-body clearfix">
                                <div class="header">
                                    <strong class="primary-font">Jack Sparrow</strong> <small class="pull-right text-muted">
                                        <span class="glyphicon glyphicon-time"></span>14 mins ago</small>
                                </div>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                        <li class="right clearfix"><span class="chat-img pull-right">
                            <img src="http://placehold.it/50/FA6F57/fff&amp;text=ME" alt="User Avatar" class="img-circle">
                        </span>
                            <div class="chat-body clearfix">
                                <div class="header">
                                    <small class=" text-muted"><span class="glyphicon glyphicon-time"></span>15 mins ago</small>
                                    <strong class="pull-right primary-font">Bhaumik Patel</strong>
                                </div>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur bibendum ornare
                                    dolor, quis ullamcorper ligula sodales.
                                </p>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="panel-footer">
                    <div class="input-group">
                        <input id="btn-input" type="text" class="form-control input-sm" placeholder="Type your message here...">
                        <span class="input-group-btn">
                            <button class="btn btn-warning btn-sm" id="btn-chat">
                                Send</button>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>

