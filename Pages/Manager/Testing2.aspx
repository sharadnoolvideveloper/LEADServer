<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testing2.aspx.cs" Inherits="Pages_Manager_Testing2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />

    <title></title>
    <style class="cp-pen-styles">
        @import url(https://fonts.googleapis.com/css?family=Open+Sans:300,400,400italic,700,700italic);
        @import url(https://fonts.googleapis.com/css?family=Lato:400,400italic,700,700italic);
        @import url(https://fonts.googleapis.com/css?family=Roboto);

        h1,
        h2,
        h3,
        h4,
        h5 {
            margin: 0;
            padding: 0;
        }

        @font-face {
            font-family: "icons";
            src: url(../fonts/icons.woff) format("woff");
            src: url(../fonts/icons.svg) format("svg");
            src: url(../fonts/icons.eot) format("eot");
            src: url(../fonts/icons.eot) format("embedded-opentype");
            src: url(../fonts/icons.ttf) format("truetype");
        }

        @font-face {
            font-family: "PragmaticaSlab";
            src: url(../fonts/PragmaticaSlab-Medium.woff) format("woff");
            src: url(../fonts/PragmaticaSlab-Medium.svg) format("svg");
            src: url(../fonts/PragmaticaSlab-Medium.eot) format("eot");
            src: url(../fonts/PragmaticaSlab-Mediumns.eot) format("embedded-opentype");
            src: url(../fonts/PragmaticaSlab-Medium.ttf) format("truetype");
        }

        html {
            display: block;
            position: relative;
            width: 100%;
            height: 100%;
        }

        body {
            display: block;
            position: relative;
            margin: 0;
            padding: 0;
            overflow-x: hidden;
            min-height: 100vh;
            background-color: #fff;
            color: #000;
            font-family: "Open Sans";
            font-size: 8px;
            line-height: 14px;
            -webkit-font-feature-settings: 'kern' 1;
            -o-font-feature-settings: 'kern' 1;
            text-rendering: geometricPrecision;
            -webkit-font-smoothing: antialiased;
        }

        header,
        section,
        footer {
            display: block;
            position: relative;
            min-width: 880px;
        }

            header .box,
            section .box,
            footer .box {
                display: block;
                position: relative;
                width: 860px;
                min-width: 860px;
                margin: 0 auto;
            }

        @media all and (max-width: 860px) {
            header,
            section,
            footer {
                width: 100% !important;
                min-width: 600px !important;
            }

                header .box,
                section .box,
                footer .box {
                    width: 100% !important;
                    min-width: 600px !important;
                    padding: 1em !important;
                }
        }

        .animated {
            -webkit-animation-duration: 1.5s;
            animation-duration: 2.5s;
            -webkit-animation-fill-mode: both;
            animation-fill-mode: both;
        }

        @-webkit-keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        .fadeIn {
            -webkit-animation-name: fadeIn;
            animation-name: fadeIn;
        }

        #features {
            text-align: center;
        }

            #features .features-title {
                display: inline-block;
                position: relative;
                color: #404040;
                font-size: 32px;
                font-family: "Roboto";
                font-weight: 400;
                line-height: 46px;
                text-transform: uppercase;
                letter-spacing: 3px;
                margin: 1em 0;
            }

                #features .features-title::after {
                    display: block;
                    position: absolute;
                    bottom: -.2em;
                    left: 50%;
                    margin-left: -40px;
                    content: "";
                    width: 80px;
                    height: 3px;
                    background-color: #00BFF3;
                }

            #features .features-content {
                display: flex;
                display: -webkit-box;
                display: -ms-flexbox;
                display: -webkit-flex;
                align-items: center;
                padding-bottom: 5em;
                margin-bottom: 5em;
                border-bottom: 2px solid #e5f7fb;
            }

                #features .features-content + .features-content {
                    border: 0;
                }

            #features .features-content-col {
                width: 50%;
                text-align: left;
            }

            #features .features-textblock {
                display: none;
                position: relative;
            }

                #features .features-textblock.__active {
                    display: block;
                }

                #features .features-textblock h1,
                #features .features-textblock h2,
                #features .features-textblock h3,
                #features .features-textblock h4,
                #features .features-textblock h5 {
                    color: #404040;
                    font-family: "Open Sans";
                    font-weight: 900;
                    padding: 0;
                    font-size: 1.5em;
                }

                #features .features-textblock p {
                    color: #404040;
                    font-family: "Open Sans";
                    font-size: 12px;
                    font-weight: 300;
                    line-height: 1.7em;
                }

                #features .features-textblock ul {
                    margin: 0;
                    padding: 0 2em;
                    list-style: none;
                }

                    #features .features-textblock ul li {
                        position: relative;
                        list-style: none;
                        margin: 0;
                        padding: 0;
                        text-indent: -5px;
                        color: #404040;
                        font-family: "Open Sans";
                        font-size: 12px;
                        font-weight: 300;
                        line-height: 1.7em;
                        padding: 0.7em 0;
                    }

                        #features .features-textblock ul li:before {
                            display: inline-block;
                            position: relative;
                            top: -1px;
                            left: -11px;
                            content: '';
                            width: 5px;
                            height: 5px;
                            background-color: #00b0de;
                            -webkit-border-radius: 50%;
                            -moz-border-radius: 50%;
                            -ms-border-radius: 50%;
                            border-radius: 50%;
                        }

            #features .features-graph {
                height: 425px;
                margin: 25px;
            }

                #features .features-graph .button-holder {
                    display: flex;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: -webkit-flex;
                    justify-content: center;
                    -webkit-justify-content: center;
                    align-items: center;
                }

                #features .features-graph .animation-holder {
                    display: flex;
                    justify-content: center;
                    align-items: center;
                }

                #features .features-graph .flash-oval {
                    background-color: #00bff3;
                    width: 12em;
                    height: 12em;
                    border-radius: 7em;
                    -webkit-transform: translateX(1px);
                    -ms-transform: translateX(1px);
                    transform: translateX(1px);
                    z-index: 100;
                    margin: 10em auto 9em auto;
                }

                    #features .features-graph .flash-oval img {
                        position: absolute;
                        right: 32px;
                        top: 23px;
                    }

                #features .features-graph .btn-with-icon {
                    display: block;
                    width: 70px;
                    height: 70px;
                    background-color: #fff;
                    border: 1px solid #9df;
                    -webkit-border-radius: 10px;
                    -moz-border-radius: 10px;
                    -ms-border-radius: 10px;
                    border-radius: 10px;
                    transition-duration: 0.3s;
                    background-position: center;
                    background-repeat: no-repeat;
                    line-height: 5em;
                    z-index: 0;
                    cursor: pointer;
                    margin-left: 8%;
                    margin-right: 8%;
                    text-align: center;
                    opacity: 0.5;
                    filter: alpha(opacity=50);
                }

                    #features .features-graph .btn-with-icon.__active {
                        opacity: 1;
                        filter: alpha(opacity=100);
                    }

                    #features .features-graph .btn-with-icon:hover {
                        opacity: 1;
                    }

                #features .features-graph .sq-bt-label {
                    letter-spacing: 0;
                    color: #656b6f;
                    font-size: 11px;
                    font-weight: 400;
                    line-height: 16px;
                    position: absolute;
                    text-transform: uppercase;
                }

                #features .features-graph .label-top-left {
                    right: 36%;
                    top: 33%;
                }

                #features .features-graph .label-top {
                    right: 21.3%;
                    top: 33%;
                }

                #features .features-graph .label-top-right {
                    right: 6.7%;
                    top: 33%;
                }

                #features .features-graph .label-bottom-left {
                    right: 36.3%;
                    bottom: 13%;
                }

                #features .features-graph .label-bottom {
                    right: 21.4%;
                    bottom: 13%;
                }

                #features .features-graph .label-bottom-right {
                    right: 6.6%;
                    bottom: 13%;
                }

                #features .features-graph .icon-features-1:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/top-left.svg);
                    background-size: 100%;
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    top: 36.4%;
                }

                #features .features-graph .icon-features-2:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/top.svg);
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    top: 36.4%;
                }

                #features .features-graph .icon-features-3:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/top-right.svg);
                    background-size: 100%;
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    top: 36.4%;
                    right: 10%;
                }

                #features .features-graph .icon-features-4:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/bottom-left.svg);
                    background-size: 100%;
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    bottom: 26%;
                }

                #features .features-graph .icon-features-5:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/bottom.svg);
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    bottom: 23%;
                }

                #features .features-graph .icon-features-6:after {
                    content: ' ';
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/bottom-right.svg);
                    background-size: 100%;
                    height: 110px;
                    width: 85px;
                    background-repeat: no-repeat;
                    position: absolute;
                    bottom: 26%;
                    right: 10%;
                }

                #features .features-graph #top-left-line {
                    position: absolute;
                    top: 170px;
                    left: 50px;
                    z-index: -4;
                }

                #features .features-graph .icon-features-1 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-1.svg);
                    background-size: 150%;
                    background-position: 50% 0;
                }

                #features .features-graph .icon-features-2 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-2.svg);
                    background-size: 70%;
                    background-position: 50% 50%;
                }

                #features .features-graph .icon-features-3 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-3.svg);
                    background-size: 65%;
                }

                #features .features-graph .icon-features-4 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-4.svg);
                    background-size: 150%;
                    background-position: 50% 0;
                }

                #features .features-graph .icon-features-5 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-5.svg);
                    background-size: 150%;
                }

                #features .features-graph .icon-features-6 {
                    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/icon-6.svg);
                    background-size: 65%;
                    background-position: 50% 50%;
                }

                #features .features-graph .wave {
                    position: absolute;
                    opacity: 0;
                    width: 12em;
                    height: 12em;
                    border: #56a9e8 1px solid;
                    border-radius: 7em;
                    -webkit-animation-name: ripple;
                    animation-name: ripple;
                    -webkit-animation-duration: 2s;
                    animation-duration: 2s;
                    -webkit-animation-iteration-count: linear;
                    animation-iteration-count: linear;
                    text-align: center;
                    top: 0;
                }

                #features .features-graph .wave2 {
                    position: absolute;
                    opacity: 0;
                    width: 12em;
                    height: 12em;
                    border: #56a9e8 1px solid;
                    border-radius: 7em;
                    -webkit-animation-name: ripple2;
                    animation-name: ripple2;
                    -webkit-animation-duration: 2s;
                    animation-duration: 2s;
                    -webkit-animation-iteration-count: linear;
                    animation-iteration-count: linear;
                    top: 0;
                    text-align: center;
                }

                #features .features-graph .wave3 {
                    position: absolute;
                    opacity: 0;
                    width: 12em;
                    height: 12em;
                    border: #56a9e8 1px solid;
                    border-radius: 7em;
                    -webkit-animation-name: ripple3;
                    animation-name: ripple3;
                    -webkit-animation-duration: 2s;
                    animation-duration: 2s;
                    -webkit-animation-iteration-count: linear;
                    animation-iteration-count: linear;
                    text-align: center;
                    top: 0;
                }

                #features .features-graph .wave4 {
                    position: absolute;
                    opacity: 0;
                    width: 12em;
                    height: 12em;
                    border: #56a9e8 1px solid;
                    border-radius: 7em;
                    -webkit-animation-name: ripple4;
                    animation-name: ripple4;
                    -webkit-animation-duration: 2s;
                    animation-duration: 2s;
                    -webkit-animation-iteration-count: linear;
                    animation-iteration-count: linear;
                    text-align: center;
                    top: 0;
                }

        @-webkit-keyframes ripple {
            from {
                opacity: 0.8;
            }

            to {
                -webkit-transform: scale(1.2);
                transform: scale(1.2);
                opacity: 0;
            }
        }

        @keyframes ripple {
            from {
                opacity: 0.8;
            }

            to {
                -webkit-transform: scale(1.2);
                transform: scale(1.2);
                opacity: 0;
            }
        }

        @-webkit-keyframes ripple2 {
            from {
                opacity: 0.7;
            }

            to {
                -webkit-transform: scale(1.5);
                transform: scale(1.5);
                opacity: 0;
            }
        }

        @keyframes ripple2 {
            from {
                opacity: 0.7;
            }

            to {
                -webkit-transform: scale(1.5);
                transform: scale(1.5);
                opacity: 0;
            }
        }

        @-webkit-keyframes ripple3 {
            from {
                opacity: 0.7;
            }

            to {
                -webkit-transform: scale(2);
                transform: scale(2);
                opacity: 0;
            }
        }

        @keyframes ripple3 {
            from {
                opacity: 0.7;
            }

            to {
                -webkit-transform: scale(2);
                transform: scale(2);
                opacity: 0;
            }
        }

        @-webkit-keyframes ripple4 {
            from {
                opacity: 0.6;
            }

            to {
                -webkit-transform: scale(2.5);
                transform: scale(2.5);
                opacity: 0;
            }
        }

        @keyframes ripple4 {
            from {
                opacity: 0.4;
            }

            to {
                -webkit-transform: scale(2.5);
                transform: scale(2.5);
                opacity: 0;
            }
        }

        #features .features-items {
            display: flex;
            display: -webkit-box;
            display: -ms-flexbox;
            display: -webkit-flex;
            flex-flow: row wrap;
            list-style: none;
            margin: 0;
            padding: 0;
        }

            #features .features-items li {
                list-style: none;
                margin: 0;
                padding: 0;
                width: 50%;
                text-align: left;
                padding: 20px 50px 20px 20px;
            }

        .hidden {
            display: none;
        }

        .visible {
            display: block;
        }

        #features .features-textblock p {
            color: #404040;
            font-family: "Open Sans";
            font-size: 12px;
            font-weight: 300;
            line-height: 1.7em;
        }

        #features .features-textblock ul {
            margin: 0;
            padding: 0 2em;
            list-style: none;
        }

            #features .features-textblock ul li {
                position: relative;
                list-style: none;
                margin: 0;
                padding: 0;
                text-indent: -5px;
                color: #404040;
                font-family: "Open Sans";
                font-size: 12px;
                font-weight: 300;
                line-height: 1.7em;
                padding: 0.7em 0;
            }

                #features .features-textblock ul li:before {
                    display: inline-block;
                    position: relative;
                    top: -1px;
                    left: -11px;
                    content: '';
                    width: 5px;
                    height: 5px;
                    background-color: #00b0de;
                    -webkit-border-radius: 50%;
                    -moz-border-radius: 50%;
                    -ms-border-radius: 50%;
                    border-radius: 50%;
                }

        #features .features-graph {
            height: 425px;
            margin: 25px;
        }

            #features .features-graph .button-holder {
                display: flex;
                display: -webkit-box;
                display: -ms-flexbox;
                display: -webkit-flex;
                justify-content: center;
                -webkit-justify-content: center;
                align-items: center;
            }

            #features .features-graph .animation-holder {
                display: flex;
                justify-content: center;
                align-items: center;
            }

            #features .features-graph .flash-oval {
                background-color: #00bff3;
                width: 12em;
                height: 12em;
                border-radius: 7em;
                -webkit-transform: translateX(1px);
                -ms-transform: translateX(1px);
                transform: translateX(1px);
                z-index: 100;
                margin: 10em auto 9em auto;
            }

                #features .features-graph .flash-oval img {
                    position: absolute;
                    right: 33px;
                    top: 25px;
                }
    </style>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" />
<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <section id="features" class="features">
            <div class="box foo">
                <h3 class="features-title">Features</h3>
                <div class="features-content">
                    <div data-features-tabs class="features-content-col">
                        <div id="feature-1" class="features-textblock animated fadeIn __active">
                            <h2>FEATURES 1</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                        <div id="feature-2" class="features-textblock animated fadeIn">
                            <h2>FEATURES 2</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                        <div id="feature-3" class="features-textblock animated fadeIn">
                            <h2>FEATURES 3</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                        <div id="feature-4" class="features-textblock animated fadeIn">
                            <h2>FEATURES 4</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                        <div id="feature-5" class="features-textblock animated fadeIn">
                            <h2>FEATURES 5</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                        <div id="feature-6" class="features-textblock animated fadeIn">
                            <h2>FEATURES 6</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            <ul>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                                <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed ipsum dolor sit amet, consectetur adipiscing elit.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="features-content-col">
                        <div data-features-nav class="features-graph">
                            <div class="button-holder"><a href="#feature-1" class="icon-features-1 btn-with-icon __active"><span class="sq-bt-label label-top-left">FEATURES 1</span></a><a href="#feature-2" class="icon-features-2 btn-with-icon"><span class="sq-bt-label label-top">FEATURES 2</span></a><a href="#feature-3" class="icon-features-3 btn-with-icon"><span class="sq-bt-label label-top-right">FEATURES 3</span></a></div>
                            <div class="animation-holder">
                                <span class="flash-oval">
                                    <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/598117/flash.png" alt="pulse">
                                    <div class="wave hidden wave-anim"></div>
                                    <div class="wave2 hidden wave-anim"></div>
                                    <div class="wave3 hidden wave-anim"></div>
                                    <div class="wave4 hidden wave-anim"></div>
                                </span>
                            </div>
                            <div class="button-holder">
                                <a href="#feature-4" class="icon-features-4 btn-with-icon">
                                    <span class="sq-bt-label label-bottom-left">FEATURES 4</span></a><a href="#feature-5" class="icon-features-5 btn-with-icon"><span class="sq-bt-label label-bottom">FEATURES 5</span></a><a href="#feature-6" class="icon-features-6 btn-with-icon"><span class="sq-bt-label label-bottom-right">FEATURES 6</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </form>


    <script>(function () {
    var selectors = {
        nav: '[data-features-nav]',
        tabs: '[data-features-tabs]',
        active: '.__active'
    }
    var classes = {
        active: '__active'
    }
    $('a', selectors.nav).on('click', function () {
        let $this = $(this)[0];
        $(selectors.active, selectors.nav).removeClass(classes.active);
        $($this).addClass(classes.active);
        $('div', selectors.tabs).removeClass(classes.active);
        $($this.hash, selectors.tabs).addClass(classes.active);
        return false
    });
}());

        $(".btn-with-icon").on("click", function () {
            $(".wave-anim").addClass('visible').one("webkitAnimationEnd mozAnimationEnd MSAnimationEnd", function () {
                $(".wave-anim").removeClass('visible');
            });
        });
        //# sourceURL=pen.js
</script>
</body>
</html>
