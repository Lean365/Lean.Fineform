<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fontawesome.aspx.cs" Inherits="LeanFine.Lf_Admin.help.fontawesome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        ul, li {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .nav {
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
        }

            .nav li {
                width: 200px;
                flex: auto;
                text-align: center;
                color: #0D47A1;
            }
        /*一.父元素属性
1.display:flex;（定义了一个flex容器）
2. flex-direction（决定主轴的方向）
      row（默认值，水平从左到右）colunm（垂直从上到下）row-reverse（水平从右到左）column-reverse（垂直从下到上）
3. flex-wrap（定义如何换行）
      nowrap（默认值，不换行）wrap（换行）wrap-reverse（换行，且颠倒行顺序，第一行在下方）
4.flex-flow（属性是 flex-direction 属性和 flex-wrap 属性的简写形式，默认值为row nowrap）
5. justify-content（设置或检索弹性盒子元素在主轴（横轴）方向上的对齐方式）
      flex-start（ 默认值、弹性盒子元素将向行起始位置对齐）
      flex-end（弹性盒子元素将向行结束位置对齐）
      center（弹性盒子元素将向行中间位置对齐。该行的子元素将相互对齐并在行中居中对齐）
      space-between（弹性盒子元素会平均地分布在行里）
      space-around（弹性盒子元素会平均地分布在行里，两端保留子元素与子元素之间间距大小的一半）
6.align-items（设置或检索弹性盒子元素在侧轴（纵轴）方向上的对齐方式）
      flex-start（弹性盒子元素的侧轴（纵轴）起始位置的边界紧靠住该行的侧轴起始边界）
      flex-end（弹性盒子元素的侧轴（纵轴）起始位置的边界紧靠住该行的侧轴结束边界）
      center（ 弹性盒子元素在该行的侧轴（纵轴）上居中放置。（如果该行的尺寸小于弹性盒子元素的尺寸，则会向两个方向溢出相同的长度））
      baseline（如弹性盒子元素的行内轴与侧轴为同一条，则该值与flex-start等效。其它情况下，该值将参与基线对齐。）
      stretch（如果指定侧轴大小的属性值为'auto'，则其值会使项目的边距盒的尺寸尽可能接近所在行的尺寸，但同时会遵照'min/max-width/height'属性的限制）
7.align-content（设置或检索弹性盒堆叠伸缩行的对齐方式）
      flex-start（各行向弹性盒容器的起始位置堆叠。弹性盒容器中第一行的侧轴起始边界紧靠住该弹性盒容器的侧轴起始边界，之后的每一行都紧靠住前面一行）
      flex-end（各行向弹性盒容器的结束位置堆叠。弹性盒容器中最后一行的侧轴起结束界紧靠住该弹性盒容器的侧轴结束边界，之后的每一行都紧靠住前面一行）
      center（各行向弹性盒容器的中间位置堆叠。各行两两紧靠住同时在弹性盒容器中居中对齐，保持弹性盒容器的侧轴起始内容边界和第一行之间的距离与该容器的侧轴结束内容边界与第最后一      行之间的距离相等）
      space-between（各行在弹性盒容器中平均分布。第一行的侧轴起始边界紧靠住弹性盒容器的侧轴起始内容边界，最后一行的侧轴结束边界紧靠住弹性盒容器的侧轴结束内容边界，剩余的行则      按一定方式在弹性盒窗口中排列，以保持两两之间的空间相等）
      space-around（ 各行在弹性盒容器中平均分布，两端保留子元素与子元素之间间距大小的一半。各行会按一定方式在弹性盒容器中排列，以保持两两之间的空间相等，同时第一行前面及最后      一行后面的空间是其他空间的一半）
      stretch（各行将会伸展以占用剩余的空间。剩余空间被所有行平分，以扩大它们的侧轴尺寸）
二.子元素上属性
1.order（默认情况下flex order会按照书写顺训呈现，可以通过order属性改变，数值小的在前面，还可以是负数）
2.flex-grow（设置或检索弹性盒的扩展比率,根据弹性盒子元素所设置的扩展因子作为比率来分配剩余空间）
3.flex-shrink（设置或检索弹性盒的收缩比率,根据弹性盒子元素所设置的收缩因子作为比率来收缩空间）
4.flex-basis (设置或检索弹性盒伸缩基准值，如果所有子元素的基准值之和大于剩余空间，则会根据每项设置的基准值，按比率伸缩剩余空间)
5.flex   (flex属性是flex-grow, flex-shrink 和 flex-basis的简写，默认值为0 1 auto。后两个属性可选)
6.align-self  (设置或检索弹性盒子元素在侧轴（纵轴）方向上的对齐方式，可以覆盖父容器align-items的设置)*/
    </style>
    <link href="~/Lf_Resources/fontawesome/css/all.css" rel="stylesheet" />
    <!--load all styles -->
</head>
<body>
    <ul class="nav">
        <li>
            <i class="fab fa-500px fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-500px</div>
        </li>
        <li>
            <i class="fab fa-accessible-icon fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-accessible-icon</div>
        </li>
        <li>
            <i class="fab fa-accusoft fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-accusoft</div>
        </li>
        <li>
            <i class="fab fa-acquisitions-incorporated fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-acquisitions-incorporated</div>
        </li>
        <li>
            <i class="fab fa-adn fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-adn</div>
        </li>
        <li>
            <i class="fab fa-adobe fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-adobe</div>
        </li>
        <li>
            <i class="fab fa-adversal fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-adversal</div>
        </li>
        <li>
            <i class="fab fa-affiliatetheme fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-affiliatetheme</div>
        </li>
        <li>
            <i class="fab fa-airbnb fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-airbnb</div>
        </li>
        <li>
            <i class="fab fa-algolia fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-algolia</div>
        </li>
        <li>
            <i class="fab fa-alipay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-alipay</div>
        </li>
        <li>
            <i class="fab fa-amazon fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-amazon</div>
        </li>
        <li>
            <i class="fab fa-amazon-pay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-amazon-pay</div>
        </li>
        <li>
            <i class="fab fa-amilia fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-amilia</div>
        </li>
        <li>
            <i class="fab fa-android fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-android</div>
        </li>
        <li>
            <i class="fab fa-angellist fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-angellist</div>
        </li>
        <li>
            <i class="fab fa-angrycreative fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-angrycreative</div>
        </li>
        <li>
            <i class="fab fa-angular fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-angular</div>
        </li>
        <li>
            <i class="fab fa-app-store fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-app-store</div>
        </li>
        <li>
            <i class="fab fa-app-store-ios fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-app-store-ios</div>
        </li>
        <li>
            <i class="fab fa-apper fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-apper</div>
        </li>
        <li>
            <i class="fab fa-apple fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-apple</div>
        </li>
        <li>
            <i class="fab fa-apple-pay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-apple-pay</div>
        </li>
        <li>
            <i class="fab fa-artstation fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-artstation</div>
        </li>
        <li>
            <i class="fab fa-asymmetrik fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-asymmetrik</div>
        </li>
        <li>
            <i class="fab fa-atlassian fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-atlassian</div>
        </li>
        <li>
            <i class="fab fa-audible fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-audible</div>
        </li>
        <li>
            <i class="fab fa-autoprefixer fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-autoprefixer</div>
        </li>
        <li>
            <i class="fab fa-avianex fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-avianex</div>
        </li>
        <li>
            <i class="fab fa-aviato fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-aviato</div>
        </li>
        <li>
            <i class="fab fa-aws fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-aws</div>
        </li>
        <li>
            <i class="fab fa-bandcamp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bandcamp</div>
        </li>
        <li>
            <i class="fab fa-battle-net fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-battle-net</div>
        </li>
        <li>
            <i class="fab fa-behance fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-behance</div>
        </li>
        <li>
            <i class="fab fa-behance-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-behance-square</div>
        </li>
        <li>
            <i class="fab fa-bimobject fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bimobject</div>
        </li>
        <li>
            <i class="fab fa-bitbucket fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bitbucket</div>
        </li>
        <li>
            <i class="fab fa-bitcoin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bitcoin</div>
        </li>
        <li>
            <i class="fab fa-bity fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bity</div>
        </li>
        <li>
            <i class="fab fa-black-tie fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-black-tie</div>
        </li>
        <li>
            <i class="fab fa-blackberry fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-blackberry</div>
        </li>
        <li>
            <i class="fab fa-blogger fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-blogger</div>
        </li>
        <li>
            <i class="fab fa-blogger-b fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-blogger-b</div>
        </li>
        <li>
            <i class="fab fa-bluetooth fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bluetooth</div>
        </li>
        <li>
            <i class="fab fa-bluetooth-b fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bluetooth-b</div>
        </li>
        <li>
            <i class="fab fa-bootstrap fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-bootstrap</div>
        </li>
        <li>
            <i class="fab fa-btc fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-btc</div>
        </li>
        <li>
            <i class="fab fa-buffer fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-buffer</div>
        </li>
        <li>
            <i class="fab fa-buromobelexperte fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-buromobelexperte</div>
        </li>
        <li>
            <i class="fab fa-buy-n-large fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-buy-n-large</div>
        </li>
        <li>
            <i class="fab fa-buysellads fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-buysellads</div>
        </li>
        <li>
            <i class="fab fa-canadian-maple-leaf fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-canadian-maple-leaf</div>
        </li>
        <li>
            <i class="fab fa-cc-amazon-pay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-amazon-pay</div>
        </li>
        <li>
            <i class="fab fa-cc-amex fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-amex</div>
        </li>
        <li>
            <i class="fab fa-cc-apple-pay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-apple-pay</div>
        </li>
        <li>
            <i class="fab fa-cc-diners-club fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-diners-club</div>
        </li>
        <li>
            <i class="fab fa-cc-discover fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-discover</div>
        </li>
        <li>
            <i class="fab fa-cc-jcb fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-jcb</div>
        </li>
        <li>
            <i class="fab fa-cc-mastercard fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-mastercard</div>
        </li>
        <li>
            <i class="fab fa-cc-paypal fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-paypal</div>
        </li>
        <li>
            <i class="fab fa-cc-stripe fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-stripe</div>
        </li>
        <li>
            <i class="fab fa-cc-visa fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cc-visa</div>
        </li>
        <li>
            <i class="fab fa-centercode fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-centercode</div>
        </li>
        <li>
            <i class="fab fa-centos fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-centos</div>
        </li>
        <li>
            <i class="fab fa-chrome fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-chrome</div>
        </li>
        <li>
            <i class="fab fa-chromecast fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-chromecast</div>
        </li>
        <li>
            <i class="fab fa-cloudscale fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cloudscale</div>
        </li>
        <li>
            <i class="fab fa-cloudsmith fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cloudsmith</div>
        </li>
        <li>
            <i class="fab fa-cloudversify fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cloudversify</div>
        </li>
        <li>
            <i class="fab fa-codepen fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-codepen</div>
        </li>
        <li>
            <i class="fab fa-codiepie fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-codiepie</div>
        </li>
        <li>
            <i class="fab fa-confluence fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-confluence</div>
        </li>
        <li>
            <i class="fab fa-connectdevelop fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-connectdevelop</div>
        </li>
        <li>
            <i class="fab fa-contao fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-contao</div>
        </li>
        <li>
            <i class="fab fa-cotton-bureau fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cotton-bureau</div>
        </li>
        <li>
            <i class="fab fa-cpanel fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cpanel</div>
        </li>
        <li>
            <i class="fab fa-creative-commons fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-by fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-by</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-nc fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-nc</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-nc-eu fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-nc-eu</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-nc-jp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-nc-jp</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-nd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-nd</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-pd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-pd</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-pd-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-pd-alt</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-remix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-remix</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-sa fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-sa</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-sampling fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-sampling</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-sampling-plus fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-sampling-plus</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-share fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-share</div>
        </li>
        <li>
            <i class="fab fa-creative-commons-zero fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-creative-commons-zero</div>
        </li>
        <li>
            <i class="fab fa-critical-role fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-critical-role</div>
        </li>
        <li>
            <i class="fab fa-css3 fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-css3</div>
        </li>
        <li>
            <i class="fab fa-css3-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-css3-alt</div>
        </li>
        <li>
            <i class="fab fa-cuttlefish fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-cuttlefish</div>
        </li>
        <li>
            <i class="fab fa-d-and-d fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-d-and-d</div>
        </li>
        <li>
            <i class="fab fa-d-and-d-beyond fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-d-and-d-beyond</div>
        </li>
        <li>
            <i class="fab fa-dashcube fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dashcube</div>
        </li>
        <li>
            <i class="fab fa-delicious fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-delicious</div>
        </li>
        <li>
            <i class="fab fa-deploydog fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-deploydog</div>
        </li>
        <li>
            <i class="fab fa-deskpro fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-deskpro</div>
        </li>
        <li>
            <i class="fab fa-dev fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dev</div>
        </li>
        <li>
            <i class="fab fa-deviantart fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-deviantart</div>
        </li>
        <li>
            <i class="fab fa-dhl fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dhl</div>
        </li>
        <li>
            <i class="fab fa-diaspora fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-diaspora</div>
        </li>
        <li>
            <i class="fab fa-digg fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-digg</div>
        </li>
        <li>
            <i class="fab fa-digital-ocean fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-digital-ocean</div>
        </li>
        <li>
            <i class="fab fa-discord fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-discord</div>
        </li>
        <li>
            <i class="fab fa-discourse fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-discourse</div>
        </li>
        <li>
            <i class="fab fa-dochub fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dochub</div>
        </li>
        <li>
            <i class="fab fa-docker fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-docker</div>
        </li>
        <li>
            <i class="fab fa-draft2digital fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-draft2digital</div>
        </li>
        <li>
            <i class="fab fa-dribbble fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dribbble</div>
        </li>
        <li>
            <i class="fab fa-dribbble-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dribbble-square</div>
        </li>
        <li>
            <i class="fab fa-dropbox fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dropbox</div>
        </li>
        <li>
            <i class="fab fa-drupal fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-drupal</div>
        </li>
        <li>
            <i class="fab fa-dyalog fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-dyalog</div>
        </li>
        <li>
            <i class="fab fa-earlybirds fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-earlybirds</div>
        </li>
        <li>
            <i class="fab fa-ebay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ebay</div>
        </li>
        <li>
            <i class="fab fa-edge fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-edge</div>
        </li>
        <li>
            <i class="fab fa-elementor fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-elementor</div>
        </li>
        <li>
            <i class="fab fa-ello fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ello</div>
        </li>
        <li>
            <i class="fab fa-ember fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ember</div>
        </li>
        <li>
            <i class="fab fa-empire fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-empire</div>
        </li>
        <li>
            <i class="fab fa-envira fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-envira</div>
        </li>
        <li>
            <i class="fab fa-erlang fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-erlang</div>
        </li>
        <li>
            <i class="fab fa-ethereum fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ethereum</div>
        </li>
        <li>
            <i class="fab fa-etsy fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-etsy</div>
        </li>
        <li>
            <i class="fab fa-evernote fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-evernote</div>
        </li>
        <li>
            <i class="fab fa-expeditedssl fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-expeditedssl</div>
        </li>
        <li>
            <i class="fab fa-facebook fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-facebook</div>
        </li>
        <li>
            <i class="fab fa-facebook-f fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-facebook-f</div>
        </li>
        <li>
            <i class="fab fa-facebook-messenger fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-facebook-messenger</div>
        </li>
        <li>
            <i class="fab fa-facebook-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-facebook-square</div>
        </li>
        <li>
            <i class="fab fa-fantasy-flight-games fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fantasy-flight-games</div>
        </li>
        <li>
            <i class="fab fa-fedex fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fedex</div>
        </li>
        <li>
            <i class="fab fa-fedora fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fedora</div>
        </li>
        <li>
            <i class="fab fa-figma fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-figma</div>
        </li>
        <li>
            <i class="fab fa-firefox fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-firefox</div>
        </li>
        <li>
            <i class="fab fa-first-order fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-first-order</div>
        </li>
        <li>
            <i class="fab fa-first-order-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-first-order-alt</div>
        </li>
        <li>
            <i class="fab fa-firstdraft fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-firstdraft</div>
        </li>
        <li>
            <i class="fab fa-flickr fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-flickr</div>
        </li>
        <li>
            <i class="fab fa-flipboard fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-flipboard</div>
        </li>
        <li>
            <i class="fab fa-fly fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fly</div>
        </li>
        <li>
            <i class="fab fa-font-awesome fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-font-awesome</div>
        </li>
        <li>
            <i class="fab fa-font-awesome-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-font-awesome-alt</div>
        </li>
        <li>
            <i class="fab fa-font-awesome-flag fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-font-awesome-flag</div>
        </li>
        <li>
            <i class="fab fa-fonticons fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fonticons</div>
        </li>
        <li>
            <i class="fab fa-fonticons-fi fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fonticons-fi</div>
        </li>
        <li>
            <i class="fab fa-fort-awesome fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fort-awesome</div>
        </li>
        <li>
            <i class="fab fa-fort-awesome-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fort-awesome-alt</div>
        </li>
        <li>
            <i class="fab fa-forumbee fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-forumbee</div>
        </li>
        <li>
            <i class="fab fa-foursquare fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-foursquare</div>
        </li>
        <li>
            <i class="fab fa-free-code-camp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-free-code-camp</div>
        </li>
        <li>
            <i class="fab fa-freebsd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-freebsd</div>
        </li>
        <li>
            <i class="fab fa-fulcrum fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-fulcrum</div>
        </li>
        <li>
            <i class="fab fa-galactic-republic fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-galactic-republic</div>
        </li>
        <li>
            <i class="fab fa-galactic-senate fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-galactic-senate</div>
        </li>
        <li>
            <i class="fab fa-get-pocket fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-get-pocket</div>
        </li>
        <li>
            <i class="fab fa-gg fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gg</div>
        </li>
        <li>
            <i class="fab fa-gg-circle fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gg-circle</div>
        </li>
        <li>
            <i class="fab fa-git fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-git</div>
        </li>
        <li>
            <i class="fab fa-git-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-git-alt</div>
        </li>
        <li>
            <i class="fab fa-git-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-git-square</div>
        </li>
        <li>
            <i class="fab fa-github fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-github</div>
        </li>
        <li>
            <i class="fab fa-github-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-github-alt</div>
        </li>
        <li>
            <i class="fab fa-github-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-github-square</div>
        </li>
        <li>
            <i class="fab fa-gitkraken fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gitkraken</div>
        </li>
        <li>
            <i class="fab fa-gitlab fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gitlab</div>
        </li>
        <li>
            <i class="fab fa-gitter fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gitter</div>
        </li>
        <li>
            <i class="fab fa-glide fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-glide</div>
        </li>
        <li>
            <i class="fab fa-glide-g fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-glide-g</div>
        </li>
        <li>
            <i class="fab fa-gofore fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gofore</div>
        </li>
        <li>
            <i class="fab fa-goodreads fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-goodreads</div>
        </li>
        <li>
            <i class="fab fa-goodreads-g fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-goodreads-g</div>
        </li>
        <li>
            <i class="fab fa-google fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google</div>
        </li>
        <li>
            <i class="fab fa-google-drive fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-drive</div>
        </li>
        <li>
            <i class="fab fa-google-play fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-play</div>
        </li>
        <li>
            <i class="fab fa-google-plus fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-plus</div>
        </li>
        <li>
            <i class="fab fa-google-plus-g fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-plus-g</div>
        </li>
        <li>
            <i class="fab fa-google-plus-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-plus-square</div>
        </li>
        <li>
            <i class="fab fa-google-wallet fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-google-wallet</div>
        </li>
        <li>
            <i class="fab fa-gratipay fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gratipay</div>
        </li>
        <li>
            <i class="fab fa-grav fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-grav</div>
        </li>
        <li>
            <i class="fab fa-gripfire fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gripfire</div>
        </li>
        <li>
            <i class="fab fa-grunt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-grunt</div>
        </li>
        <li>
            <i class="fab fa-gulp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-gulp</div>
        </li>
        <li>
            <i class="fab fa-hacker-news fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hacker-news</div>
        </li>
        <li>
            <i class="fab fa-hacker-news-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hacker-news-square</div>
        </li>
        <li>
            <i class="fab fa-hackerrank fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hackerrank</div>
        </li>
        <li>
            <i class="fab fa-hips fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hips</div>
        </li>
        <li>
            <i class="fab fa-hire-a-helper fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hire-a-helper</div>
        </li>
        <li>
            <i class="fab fa-hooli fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hooli</div>
        </li>
        <li>
            <i class="fab fa-hornbill fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hornbill</div>
        </li>
        <li>
            <i class="fab fa-hotjar fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hotjar</div>
        </li>
        <li>
            <i class="fab fa-houzz fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-houzz</div>
        </li>
        <li>
            <i class="fab fa-html5 fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-html5</div>
        </li>
        <li>
            <i class="fab fa-hubspot fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-hubspot</div>
        </li>
        <li>
            <i class="fab fa-imdb fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-imdb</div>
        </li>
        <li>
            <i class="fab fa-instagram fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-instagram</div>
        </li>
        <li>
            <i class="fab fa-intercom fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-intercom</div>
        </li>
        <li>
            <i class="fab fa-internet-explorer fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-internet-explorer</div>
        </li>
        <li>
            <i class="fab fa-invision fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-invision</div>
        </li>
        <li>
            <i class="fab fa-ioxhost fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ioxhost</div>
        </li>
        <li>
            <i class="fab fa-itch-io fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-itch-io</div>
        </li>
        <li>
            <i class="fab fa-itunes fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-itunes</div>
        </li>
        <li>
            <i class="fab fa-itunes-note fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-itunes-note</div>
        </li>
        <li>
            <i class="fab fa-java fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-java</div>
        </li>
        <li>
            <i class="fab fa-jedi-order fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-jedi-order</div>
        </li>
        <li>
            <i class="fab fa-jenkins fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-jenkins</div>
        </li>
        <li>
            <i class="fab fa-jira fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-jira</div>
        </li>
        <li>
            <i class="fab fa-joget fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-joget</div>
        </li>
        <li>
            <i class="fab fa-joomla fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-joomla</div>
        </li>
        <li>
            <i class="fab fa-js fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-js</div>
        </li>
        <li>
            <i class="fab fa-js-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-js-square</div>
        </li>
        <li>
            <i class="fab fa-jsfiddle fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-jsfiddle</div>
        </li>
        <li>
            <i class="fab fa-kaggle fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-kaggle</div>
        </li>
        <li>
            <i class="fab fa-keybase fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-keybase</div>
        </li>
        <li>
            <i class="fab fa-keycdn fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-keycdn</div>
        </li>
        <li>
            <i class="fab fa-kickstarter fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-kickstarter</div>
        </li>
        <li>
            <i class="fab fa-kickstarter-k fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-kickstarter-k</div>
        </li>
        <li>
            <i class="fab fa-korvue fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-korvue</div>
        </li>
        <li>
            <i class="fab fa-laravel fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-laravel</div>
        </li>
        <li>
            <i class="fab fa-lastfm fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-lastfm</div>
        </li>
        <li>
            <i class="fab fa-lastfm-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-lastfm-square</div>
        </li>
        <li>
            <i class="fab fa-leanpub fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-leanpub</div>
        </li>
        <li>
            <i class="fab fa-less fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-less</div>
        </li>
        <li>
            <i class="fab fa-line fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-line</div>
        </li>
        <li>
            <i class="fab fa-linkedin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-linkedin</div>
        </li>
        <li>
            <i class="fab fa-linkedin-in fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-linkedin-in</div>
        </li>
        <li>
            <i class="fab fa-linode fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-linode</div>
        </li>
        <li>
            <i class="fab fa-linux fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-linux</div>
        </li>
        <li>
            <i class="fab fa-lyft fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-lyft</div>
        </li>
        <li>
            <i class="fab fa-magento fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-magento</div>
        </li>
        <li>
            <i class="fab fa-mailchimp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mailchimp</div>
        </li>
        <li>
            <i class="fab fa-mandalorian fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mandalorian</div>
        </li>
        <li>
            <i class="fab fa-markdown fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-markdown</div>
        </li>
        <li>
            <i class="fab fa-mastodon fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mastodon</div>
        </li>
        <li>
            <i class="fab fa-maxcdn fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-maxcdn</div>
        </li>
        <li>
            <i class="fab fa-mdb fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mdb</div>
        </li>
        <li>
            <i class="fab fa-medapps fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-medapps</div>
        </li>
        <li>
            <i class="fab fa-medium fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-medium</div>
        </li>
        <li>
            <i class="fab fa-medium-m fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-medium-m</div>
        </li>
        <li>
            <i class="fab fa-medrt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-medrt</div>
        </li>
        <li>
            <i class="fab fa-meetup fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-meetup</div>
        </li>
        <li>
            <i class="fab fa-megaport fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-megaport</div>
        </li>
        <li>
            <i class="fab fa-mendeley fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mendeley</div>
        </li>
        <li>
            <i class="fab fa-microsoft fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-microsoft</div>
        </li>
        <li>
            <i class="fab fa-mix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mix</div>
        </li>
        <li>
            <i class="fab fa-mixcloud fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mixcloud</div>
        </li>
        <li>
            <i class="fab fa-mizuni fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-mizuni</div>
        </li>
        <li>
            <i class="fab fa-modx fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-modx</div>
        </li>
        <li>
            <i class="fab fa-monero fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-monero</div>
        </li>
        <li>
            <i class="fab fa-napster fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-napster</div>
        </li>
        <li>
            <i class="fab fa-neos fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-neos</div>
        </li>
        <li>
            <i class="fab fa-nimblr fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-nimblr</div>
        </li>
        <li>
            <i class="fab fa-node fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-node</div>
        </li>
        <li>
            <i class="fab fa-node-js fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-node-js</div>
        </li>
        <li>
            <i class="fab fa-npm fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-npm</div>
        </li>
        <li>
            <i class="fab fa-ns8 fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ns8</div>
        </li>
        <li>
            <i class="fab fa-nutritionix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-nutritionix</div>
        </li>
        <li>
            <i class="fab fa-odnoklassniki fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-odnoklassniki</div>
        </li>
        <li>
            <i class="fab fa-odnoklassniki-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-odnoklassniki-square</div>
        </li>
        <li>
            <i class="fab fa-old-republic fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-old-republic</div>
        </li>
        <li>
            <i class="fab fa-opencart fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-opencart</div>
        </li>
        <li>
            <i class="fab fa-openid fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-openid</div>
        </li>
        <li>
            <i class="fab fa-opera fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-opera</div>
        </li>
        <li>
            <i class="fab fa-optin-monster fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-optin-monster</div>
        </li>
        <li>
            <i class="fab fa-orcid fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-orcid</div>
        </li>
        <li>
            <i class="fab fa-osi fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-osi</div>
        </li>
        <li>
            <i class="fab fa-page4 fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-page4</div>
        </li>
        <li>
            <i class="fab fa-pagelines fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pagelines</div>
        </li>
        <li>
            <i class="fab fa-palfed fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-palfed</div>
        </li>
        <li>
            <i class="fab fa-patreon fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-patreon</div>
        </li>
        <li>
            <i class="fab fa-paypal fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-paypal</div>
        </li>
        <li>
            <i class="fab fa-penny-arcade fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-penny-arcade</div>
        </li>
        <li>
            <i class="fab fa-periscope fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-periscope</div>
        </li>
        <li>
            <i class="fab fa-phabricator fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-phabricator</div>
        </li>
        <li>
            <i class="fab fa-phoenix-framework fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-phoenix-framework</div>
        </li>
        <li>
            <i class="fab fa-phoenix-squadron fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-phoenix-squadron</div>
        </li>
        <li>
            <i class="fab fa-php fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-php</div>
        </li>
        <li>
            <i class="fab fa-pied-piper fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pied-piper</div>
        </li>
        <li>
            <i class="fab fa-pied-piper-alt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pied-piper-alt</div>
        </li>
        <li>
            <i class="fab fa-pied-piper-hat fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pied-piper-hat</div>
        </li>
        <li>
            <i class="fab fa-pied-piper-pp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pied-piper-pp</div>
        </li>
        <li>
            <i class="fab fa-pinterest fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pinterest</div>
        </li>
        <li>
            <i class="fab fa-pinterest-p fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pinterest-p</div>
        </li>
        <li>
            <i class="fab fa-pinterest-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pinterest-square</div>
        </li>
        <li>
            <i class="fab fa-playstation fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-playstation</div>
        </li>
        <li>
            <i class="fab fa-product-hunt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-product-hunt</div>
        </li>
        <li>
            <i class="fab fa-pushed fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-pushed</div>
        </li>
        <li>
            <i class="fab fa-python fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-python</div>
        </li>
        <li>
            <i class="fab fa-qq fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-qq</div>
        </li>
        <li>
            <i class="fab fa-quinscape fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-quinscape</div>
        </li>
        <li>
            <i class="fab fa-quora fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-quora</div>
        </li>
        <li>
            <i class="fab fa-r-project fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-r-project</div>
        </li>
        <li>
            <i class="fab fa-raspberry-pi fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-raspberry-pi</div>
        </li>
        <li>
            <i class="fab fa-ravelry fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ravelry</div>
        </li>
        <li>
            <i class="fab fa-react fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-react</div>
        </li>
        <li>
            <i class="fab fa-reacteurope fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-reacteurope</div>
        </li>
        <li>
            <i class="fab fa-readme fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-readme</div>
        </li>
        <li>
            <i class="fab fa-rebel fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-rebel</div>
        </li>
        <li>
            <i class="fab fa-red-river fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-red-river</div>
        </li>
        <li>
            <i class="fab fa-reddit fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-reddit</div>
        </li>
        <li>
            <i class="fab fa-reddit-alien fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-reddit-alien</div>
        </li>
        <li>
            <i class="fab fa-reddit-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-reddit-square</div>
        </li>
        <li>
            <i class="fab fa-redhat fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-redhat</div>
        </li>
        <li>
            <i class="fab fa-renren fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-renren</div>
        </li>
        <li>
            <i class="fab fa-replyd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-replyd</div>
        </li>
        <li>
            <i class="fab fa-researchgate fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-researchgate</div>
        </li>
        <li>
            <i class="fab fa-resolving fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-resolving</div>
        </li>
        <li>
            <i class="fab fa-rev fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-rev</div>
        </li>
        <li>
            <i class="fab fa-rocketchat fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-rocketchat</div>
        </li>
        <li>
            <i class="fab fa-rockrms fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-rockrms</div>
        </li>
        <li>
            <i class="fab fa-safari fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-safari</div>
        </li>
        <li>
            <i class="fab fa-salesforce fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-salesforce</div>
        </li>
        <li>
            <i class="fab fa-sass fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sass</div>
        </li>
        <li>
            <i class="fab fa-schlix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-schlix</div>
        </li>
        <li>
            <i class="fab fa-scribd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-scribd</div>
        </li>
        <li>
            <i class="fab fa-searchengin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-searchengin</div>
        </li>
        <li>
            <i class="fab fa-sellcast fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sellcast</div>
        </li>
        <li>
            <i class="fab fa-sellsy fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sellsy</div>
        </li>
        <li>
            <i class="fab fa-servicestack fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-servicestack</div>
        </li>
        <li>
            <i class="fab fa-shirtsinbulk fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-shirtsinbulk</div>
        </li>
        <li>
            <i class="fab fa-shopware fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-shopware</div>
        </li>
        <li>
            <i class="fab fa-simplybuilt fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-simplybuilt</div>
        </li>
        <li>
            <i class="fab fa-sistrix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sistrix</div>
        </li>
        <li>
            <i class="fab fa-sith fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sith</div>
        </li>
        <li>
            <i class="fab fa-sketch fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sketch</div>
        </li>
        <li>
            <i class="fab fa-skyatlas fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-skyatlas</div>
        </li>
        <li>
            <i class="fab fa-skype fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-skype</div>
        </li>
        <li>
            <i class="fab fa-slack fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-slack</div>
        </li>
        <li>
            <i class="fab fa-slack-hash fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-slack-hash</div>
        </li>
        <li>
            <i class="fab fa-slideshare fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-slideshare</div>
        </li>
        <li>
            <i class="fab fa-snapchat fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-snapchat</div>
        </li>
        <li>
            <i class="fab fa-snapchat-ghost fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-snapchat-ghost</div>
        </li>
        <li>
            <i class="fab fa-snapchat-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-snapchat-square</div>
        </li>
        <li>
            <i class="fab fa-soundcloud fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-soundcloud</div>
        </li>
        <li>
            <i class="fab fa-sourcetree fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sourcetree</div>
        </li>
        <li>
            <i class="fab fa-speakap fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-speakap</div>
        </li>
        <li>
            <i class="fab fa-speaker-deck fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-speaker-deck</div>
        </li>
        <li>
            <i class="fab fa-spotify fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-spotify</div>
        </li>
        <li>
            <i class="fab fa-squarespace fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-squarespace</div>
        </li>
        <li>
            <i class="fab fa-stack-exchange fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stack-exchange</div>
        </li>
        <li>
            <i class="fab fa-stack-overflow fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stack-overflow</div>
        </li>
        <li>
            <i class="fab fa-stackpath fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stackpath</div>
        </li>
        <li>
            <i class="fab fa-staylinked fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-staylinked</div>
        </li>
        <li>
            <i class="fab fa-steam fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-steam</div>
        </li>
        <li>
            <i class="fab fa-steam-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-steam-square</div>
        </li>
        <li>
            <i class="fab fa-steam-symbol fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-steam-symbol</div>
        </li>
        <li>
            <i class="fab fa-sticker-mule fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-sticker-mule</div>
        </li>
        <li>
            <i class="fab fa-strava fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-strava</div>
        </li>
        <li>
            <i class="fab fa-stripe fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stripe</div>
        </li>
        <li>
            <i class="fab fa-stripe-s fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stripe-s</div>
        </li>
        <li>
            <i class="fab fa-studiovinari fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-studiovinari</div>
        </li>
        <li>
            <i class="fab fa-stumbleupon fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stumbleupon</div>
        </li>
        <li>
            <i class="fab fa-stumbleupon-circle fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-stumbleupon-circle</div>
        </li>
        <li>
            <i class="fab fa-superpowers fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-superpowers</div>
        </li>
        <li>
            <i class="fab fa-supple fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-supple</div>
        </li>
        <li>
            <i class="fab fa-suse fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-suse</div>
        </li>
        <li>
            <i class="fab fa-swift fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-swift</div>
        </li>
        <li>
            <i class="fab fa-symfony fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-symfony</div>
        </li>
        <li>
            <i class="fab fa-teamspeak fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-teamspeak</div>
        </li>
        <li>
            <i class="fab fa-telegram fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-telegram</div>
        </li>
        <li>
            <i class="fab fa-telegram-plane fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-telegram-plane</div>
        </li>
        <li>
            <i class="fab fa-tencent-weibo fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-tencent-weibo</div>
        </li>
        <li>
            <i class="fab fa-the-red-yeti fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-the-red-yeti</div>
        </li>
        <li>
            <i class="fab fa-themeco fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-themeco</div>
        </li>
        <li>
            <i class="fab fa-themeisle fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-themeisle</div>
        </li>
        <li>
            <i class="fab fa-think-peaks fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-think-peaks</div>
        </li>
        <li>
            <i class="fab fa-trade-federation fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-trade-federation</div>
        </li>
        <li>
            <i class="fab fa-trello fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-trello</div>
        </li>
        <li>
            <i class="fab fa-tripadvisor fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-tripadvisor</div>
        </li>
        <li>
            <i class="fab fa-tumblr fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-tumblr</div>
        </li>
        <li>
            <i class="fab fa-tumblr-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-tumblr-square</div>
        </li>
        <li>
            <i class="fab fa-twitch fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-twitch</div>
        </li>
        <li>
            <i class="fab fa-twitter fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-twitter</div>
        </li>
        <li>
            <i class="fab fa-twitter-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-twitter-square</div>
        </li>
        <li>
            <i class="fab fa-typo3 fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-typo3</div>
        </li>
        <li>
            <i class="fab fa-uber fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-uber</div>
        </li>
        <li>
            <i class="fab fa-ubuntu fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ubuntu</div>
        </li>
        <li>
            <i class="fab fa-uikit fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-uikit</div>
        </li>
        <li>
            <i class="fab fa-umbraco fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-umbraco</div>
        </li>
        <li>
            <i class="fab fa-uniregistry fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-uniregistry</div>
        </li>
        <li>
            <i class="fab fa-untappd fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-untappd</div>
        </li>
        <li>
            <i class="fab fa-ups fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ups</div>
        </li>
        <li>
            <i class="fab fa-usb fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-usb</div>
        </li>
        <li>
            <i class="fab fa-usps fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-usps</div>
        </li>
        <li>
            <i class="fab fa-ussunnah fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-ussunnah</div>
        </li>
        <li>
            <i class="fab fa-vaadin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vaadin</div>
        </li>
        <li>
            <i class="fab fa-viacoin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-viacoin</div>
        </li>
        <li>
            <i class="fab fa-viadeo fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-viadeo</div>
        </li>
        <li>
            <i class="fab fa-viadeo-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-viadeo-square</div>
        </li>
        <li>
            <i class="fab fa-viber fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-viber</div>
        </li>
        <li>
            <i class="fab fa-vimeo fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vimeo</div>
        </li>
        <li>
            <i class="fab fa-vimeo-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vimeo-square</div>
        </li>
        <li>
            <i class="fab fa-vimeo-v fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vimeo-v</div>
        </li>
        <li>
            <i class="fab fa-vine fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vine</div>
        </li>
        <li>
            <i class="fab fa-vk fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vk</div>
        </li>
        <li>
            <i class="fab fa-vnv fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vnv</div>
        </li>
        <li>
            <i class="fab fa-vuejs fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-vuejs</div>
        </li>
        <li>
            <i class="fab fa-waze fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-waze</div>
        </li>
        <li>
            <i class="fab fa-weebly fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-weebly</div>
        </li>
        <li>
            <i class="fab fa-weibo fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-weibo</div>
        </li>
        <li>
            <i class="fab fa-weixin fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-weixin</div>
        </li>
        <li>
            <i class="fab fa-whatsapp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-whatsapp</div>
        </li>
        <li>
            <i class="fab fa-whatsapp-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-whatsapp-square</div>
        </li>
        <li>
            <i class="fab fa-whmcs fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-whmcs</div>
        </li>
        <li>
            <i class="fab fa-wikipedia-w fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wikipedia-w</div>
        </li>
        <li>
            <i class="fab fa-windows fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-windows</div>
        </li>
        <li>
            <i class="fab fa-wix fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wix</div>
        </li>
        <li>
            <i class="fab fa-wizards-of-the-coast fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wizards-of-the-coast</div>
        </li>
        <li>
            <i class="fab fa-wolf-pack-battalion fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wolf-pack-battalion</div>
        </li>
        <li>
            <i class="fab fa-wordpress fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wordpress</div>
        </li>
        <li>
            <i class="fab fa-wordpress-simple fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wordpress-simple</div>
        </li>
        <li>
            <i class="fab fa-wpbeginner fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wpbeginner</div>
        </li>
        <li>
            <i class="fab fa-wpexplorer fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wpexplorer</div>
        </li>
        <li>
            <i class="fab fa-wpforms fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wpforms</div>
        </li>
        <li>
            <i class="fab fa-wpressr fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-wpressr</div>
        </li>
        <li>
            <i class="fab fa-xbox fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-xbox</div>
        </li>
        <li>
            <i class="fab fa-xing fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-xing</div>
        </li>
        <li>
            <i class="fab fa-xing-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-xing-square</div>
        </li>
        <li>
            <i class="fab fa-y-combinator fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-y-combinator</div>
        </li>
        <li>
            <i class="fab fa-yahoo fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yahoo</div>
        </li>
        <li>
            <i class="fab fa-yammer fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yammer</div>
        </li>
        <li>
            <i class="fab fa-yandex fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yandex</div>
        </li>
        <li>
            <i class="fab fa-yandex-international fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yandex-international</div>
        </li>
        <li>
            <i class="fab fa-yarn fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yarn</div>
        </li>
        <li>
            <i class="fab fa-yelp fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yelp</div>
        </li>
        <li>
            <i class="fab fa-yoast fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-yoast</div>
        </li>
        <li>
            <i class="fab fa-youtube fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-youtube</div>
        </li>
        <li>
            <i class="fab fa-youtube-square fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-youtube-square</div>
        </li>
        <li>
            <i class="fab fa-zhihu fa-2x"></i>
            <!-- uses Brands style -->
            <div>fab fa-zhihu</div>
        </li>
        <li>
            <i class="fas fa-ad fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ad</div>
        </li>
        <li>
            <i class="fas fa-address-book fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-address-book</div>
        </li>
        <li>
            <i class="fas fa-address-card fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-address-card</div>
        </li>
        <li>
            <i class="fas fa-adjust fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-adjust</div>
        </li>
        <li>
            <i class="fas fa-air-freshener fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-air-freshener</div>
        </li>
        <li>
            <i class="fas fa-align-center fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-align-center</div>
        </li>
        <li>
            <i class="fas fa-align-justify fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-align-justify</div>
        </li>
        <li>
            <i class="fas fa-align-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-align-left</div>
        </li>
        <li>
            <i class="fas fa-align-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-align-right</div>
        </li>
        <li>
            <i class="fas fa-allergies fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-allergies</div>
        </li>
        <li>
            <i class="fas fa-ambulance fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ambulance</div>
        </li>
        <li>
            <i class="fas fa-american-sign-language-interpreting fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-american-sign-language-interpreting</div>
        </li>
        <li>
            <i class="fas fa-anchor fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-anchor</div>
        </li>
        <li>
            <i class="fas fa-angle-double-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-double-down</div>
        </li>
        <li>
            <i class="fas fa-angle-double-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-double-left</div>
        </li>
        <li>
            <i class="fas fa-angle-double-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-double-right</div>
        </li>
        <li>
            <i class="fas fa-angle-double-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-double-up</div>
        </li>
        <li>
            <i class="fas fa-angle-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-down</div>
        </li>
        <li>
            <i class="fas fa-angle-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-left</div>
        </li>
        <li>
            <i class="fas fa-angle-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-right</div>
        </li>
        <li>
            <i class="fas fa-angle-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angle-up</div>
        </li>
        <li>
            <i class="fas fa-angry fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-angry</div>
        </li>
        <li>
            <i class="fas fa-ankh fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ankh</div>
        </li>
        <li>
            <i class="fas fa-apple-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-apple-alt</div>
        </li>
        <li>
            <i class="fas fa-archive fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-archive</div>
        </li>
        <li>
            <i class="fas fa-archway fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-archway</div>
        </li>
        <li>
            <i class="fas fa-arrow-alt-circle-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-alt-circle-down</div>
        </li>
        <li>
            <i class="fas fa-arrow-alt-circle-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-alt-circle-left</div>
        </li>
        <li>
            <i class="fas fa-arrow-alt-circle-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-alt-circle-right</div>
        </li>
        <li>
            <i class="fas fa-arrow-alt-circle-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-alt-circle-up</div>
        </li>
        <li>
            <i class="fas fa-arrow-circle-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-circle-down</div>
        </li>
        <li>
            <i class="fas fa-arrow-circle-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-circle-left</div>
        </li>
        <li>
            <i class="fas fa-arrow-circle-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-circle-right</div>
        </li>
        <li>
            <i class="fas fa-arrow-circle-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-circle-up</div>
        </li>
        <li>
            <i class="fas fa-arrow-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-down</div>
        </li>
        <li>
            <i class="fas fa-arrow-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-left</div>
        </li>
        <li>
            <i class="fas fa-arrow-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-right</div>
        </li>
        <li>
            <i class="fas fa-arrow-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrow-up</div>
        </li>
        <li>
            <i class="fas fa-arrows-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrows-alt</div>
        </li>
        <li>
            <i class="fas fa-arrows-alt-h fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrows-alt-h</div>
        </li>
        <li>
            <i class="fas fa-arrows-alt-v fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-arrows-alt-v</div>
        </li>
        <li>
            <i class="fas fa-assistive-listening-systems fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-assistive-listening-systems</div>
        </li>
        <li>
            <i class="fas fa-asterisk fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-asterisk</div>
        </li>
        <li>
            <i class="fas fa-at fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-at</div>
        </li>
        <li>
            <i class="fas fa-atlas fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-atlas</div>
        </li>
        <li>
            <i class="fas fa-atom fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-atom</div>
        </li>
        <li>
            <i class="fas fa-audio-description fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-audio-description</div>
        </li>
        <li>
            <i class="fas fa-award fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-award</div>
        </li>
        <li>
            <i class="fas fa-baby fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-baby</div>
        </li>
        <li>
            <i class="fas fa-baby-carriage fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-baby-carriage</div>
        </li>
        <li>
            <i class="fas fa-backspace fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-backspace</div>
        </li>
        <li>
            <i class="fas fa-backward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-backward</div>
        </li>
        <li>
            <i class="fas fa-bacon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bacon</div>
        </li>
        <li>
            <i class="fas fa-balance-scale fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-balance-scale</div>
        </li>
        <li>
            <i class="fas fa-balance-scale-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-balance-scale-left</div>
        </li>
        <li>
            <i class="fas fa-balance-scale-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-balance-scale-right</div>
        </li>
        <li>
            <i class="fas fa-ban fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ban</div>
        </li>
        <li>
            <i class="fas fa-band-aid fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-band-aid</div>
        </li>
        <li>
            <i class="fas fa-barcode fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-barcode</div>
        </li>
        <li>
            <i class="fas fa-bars fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bars</div>
        </li>
        <li>
            <i class="fas fa-baseball-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-baseball-ball</div>
        </li>
        <li>
            <i class="fas fa-basketball-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-basketball-ball</div>
        </li>
        <li>
            <i class="fas fa-bath fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bath</div>
        </li>
        <li>
            <i class="fas fa-battery-empty fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-battery-empty</div>
        </li>
        <li>
            <i class="fas fa-battery-full fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-battery-full</div>
        </li>
        <li>
            <i class="fas fa-battery-half fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-battery-half</div>
        </li>
        <li>
            <i class="fas fa-battery-quarter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-battery-quarter</div>
        </li>
        <li>
            <i class="fas fa-battery-three-quarters fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-battery-three-quarters</div>
        </li>
        <li>
            <i class="fas fa-bed fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bed</div>
        </li>
        <li>
            <i class="fas fa-beer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-beer</div>
        </li>
        <li>
            <i class="fas fa-bell fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bell</div>
        </li>
        <li>
            <i class="fas fa-bell-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bell-slash</div>
        </li>
        <li>
            <i class="fas fa-bezier-curve fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bezier-curve</div>
        </li>
        <li>
            <i class="fas fa-bible fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bible</div>
        </li>
        <li>
            <i class="fas fa-bicycle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bicycle</div>
        </li>
        <li>
            <i class="fas fa-biking fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-biking</div>
        </li>
        <li>
            <i class="fas fa-binoculars fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-binoculars</div>
        </li>
        <li>
            <i class="fas fa-biohazard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-biohazard</div>
        </li>
        <li>
            <i class="fas fa-birthday-cake fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-birthday-cake</div>
        </li>
        <li>
            <i class="fas fa-blender fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-blender</div>
        </li>
        <li>
            <i class="fas fa-blender-phone fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-blender-phone</div>
        </li>
        <li>
            <i class="fas fa-blind fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-blind</div>
        </li>
        <li>
            <i class="fas fa-blog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-blog</div>
        </li>
        <li>
            <i class="fas fa-bold fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bold</div>
        </li>
        <li>
            <i class="fas fa-bolt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bolt</div>
        </li>
        <li>
            <i class="fas fa-bomb fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bomb</div>
        </li>
        <li>
            <i class="fas fa-bone fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bone</div>
        </li>
        <li>
            <i class="fas fa-bong fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bong</div>
        </li>
        <li>
            <i class="fas fa-book fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-book</div>
        </li>
        <li>
            <i class="fas fa-book-dead fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-book-dead</div>
        </li>
        <li>
            <i class="fas fa-book-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-book-medical</div>
        </li>
        <li>
            <i class="fas fa-book-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-book-open</div>
        </li>
        <li>
            <i class="fas fa-book-reader fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-book-reader</div>
        </li>
        <li>
            <i class="fas fa-bookmark fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bookmark</div>
        </li>
        <li>
            <i class="fas fa-border-all fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-border-all</div>
        </li>
        <li>
            <i class="fas fa-border-none fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-border-none</div>
        </li>
        <li>
            <i class="fas fa-border-style fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-border-style</div>
        </li>
        <li>
            <i class="fas fa-bowling-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bowling-ball</div>
        </li>
        <li>
            <i class="fas fa-box fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-box</div>
        </li>
        <li>
            <i class="fas fa-box-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-box-open</div>
        </li>
        <li>
            <i class="fas fa-boxes fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-boxes</div>
        </li>
        <li>
            <i class="fas fa-braille fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-braille</div>
        </li>
        <li>
            <i class="fas fa-brain fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-brain</div>
        </li>
        <li>
            <i class="fas fa-bread-slice fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bread-slice</div>
        </li>
        <li>
            <i class="fas fa-briefcase fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-briefcase</div>
        </li>
        <li>
            <i class="fas fa-briefcase-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-briefcase-medical</div>
        </li>
        <li>
            <i class="fas fa-broadcast-tower fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-broadcast-tower</div>
        </li>
        <li>
            <i class="fas fa-broom fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-broom</div>
        </li>
        <li>
            <i class="fas fa-brush fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-brush</div>
        </li>
        <li>
            <i class="fas fa-bug fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bug</div>
        </li>
        <li>
            <i class="fas fa-building fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-building</div>
        </li>
        <li>
            <i class="fas fa-bullhorn fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bullhorn</div>
        </li>
        <li>
            <i class="fas fa-bullseye fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bullseye</div>
        </li>
        <li>
            <i class="fas fa-burn fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-burn</div>
        </li>
        <li>
            <i class="fas fa-bus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bus</div>
        </li>
        <li>
            <i class="fas fa-bus-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-bus-alt</div>
        </li>
        <li>
            <i class="fas fa-business-time fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-business-time</div>
        </li>
        <li>
            <i class="fas fa-calculator fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calculator</div>
        </li>
        <li>
            <i class="fas fa-calendar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar</div>
        </li>
        <li>
            <i class="fas fa-calendar-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-alt</div>
        </li>
        <li>
            <i class="fas fa-calendar-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-check</div>
        </li>
        <li>
            <i class="fas fa-calendar-day fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-day</div>
        </li>
        <li>
            <i class="fas fa-calendar-minus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-minus</div>
        </li>
        <li>
            <i class="fas fa-calendar-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-plus</div>
        </li>
        <li>
            <i class="fas fa-calendar-times fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-times</div>
        </li>
        <li>
            <i class="fas fa-calendar-week fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-calendar-week</div>
        </li>
        <li>
            <i class="fas fa-camera fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-camera</div>
        </li>
        <li>
            <i class="fas fa-camera-retro fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-camera-retro</div>
        </li>
        <li>
            <i class="fas fa-campground fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-campground</div>
        </li>
        <li>
            <i class="fas fa-candy-cane fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-candy-cane</div>
        </li>
        <li>
            <i class="fas fa-cannabis fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cannabis</div>
        </li>
        <li>
            <i class="fas fa-capsules fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-capsules</div>
        </li>
        <li>
            <i class="fas fa-car fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-car</div>
        </li>
        <li>
            <i class="fas fa-car-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-car-alt</div>
        </li>
        <li>
            <i class="fas fa-car-battery fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-car-battery</div>
        </li>
        <li>
            <i class="fas fa-car-crash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-car-crash</div>
        </li>
        <li>
            <i class="fas fa-car-side fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-car-side</div>
        </li>
        <li>
            <i class="fas fa-caret-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-down</div>
        </li>
        <li>
            <i class="fas fa-caret-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-left</div>
        </li>
        <li>
            <i class="fas fa-caret-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-right</div>
        </li>
        <li>
            <i class="fas fa-caret-square-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-square-down</div>
        </li>
        <li>
            <i class="fas fa-caret-square-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-square-left</div>
        </li>
        <li>
            <i class="fas fa-caret-square-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-square-right</div>
        </li>
        <li>
            <i class="fas fa-caret-square-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-square-up</div>
        </li>
        <li>
            <i class="fas fa-caret-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-caret-up</div>
        </li>
        <li>
            <i class="fas fa-carrot fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-carrot</div>
        </li>
        <li>
            <i class="fas fa-cart-arrow-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cart-arrow-down</div>
        </li>
        <li>
            <i class="fas fa-cart-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cart-plus</div>
        </li>
        <li>
            <i class="fas fa-cash-register fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cash-register</div>
        </li>
        <li>
            <i class="fas fa-cat fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cat</div>
        </li>
        <li>
            <i class="fas fa-certificate fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-certificate</div>
        </li>
        <li>
            <i class="fas fa-chair fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chair</div>
        </li>
        <li>
            <i class="fas fa-chalkboard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chalkboard</div>
        </li>
        <li>
            <i class="fas fa-chalkboard-teacher fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chalkboard-teacher</div>
        </li>
        <li>
            <i class="fas fa-charging-station fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-charging-station</div>
        </li>
        <li>
            <i class="fas fa-chart-area fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chart-area</div>
        </li>
        <li>
            <i class="fas fa-chart-bar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chart-bar</div>
        </li>
        <li>
            <i class="fas fa-chart-line fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chart-line</div>
        </li>
        <li>
            <i class="fas fa-chart-pie fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chart-pie</div>
        </li>
        <li>
            <i class="fas fa-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-check</div>
        </li>
        <li>
            <i class="fas fa-check-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-check-circle</div>
        </li>
        <li>
            <i class="fas fa-check-double fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-check-double</div>
        </li>
        <li>
            <i class="fas fa-check-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-check-square</div>
        </li>
        <li>
            <i class="fas fa-cheese fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cheese</div>
        </li>
        <li>
            <i class="fas fa-chess fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess</div>
        </li>
        <li>
            <i class="fas fa-chess-bishop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-bishop</div>
        </li>
        <li>
            <i class="fas fa-chess-board fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-board</div>
        </li>
        <li>
            <i class="fas fa-chess-king fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-king</div>
        </li>
        <li>
            <i class="fas fa-chess-knight fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-knight</div>
        </li>
        <li>
            <i class="fas fa-chess-pawn fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-pawn</div>
        </li>
        <li>
            <i class="fas fa-chess-queen fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-queen</div>
        </li>
        <li>
            <i class="fas fa-chess-rook fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chess-rook</div>
        </li>
        <li>
            <i class="fas fa-chevron-circle-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-circle-down</div>
        </li>
        <li>
            <i class="fas fa-chevron-circle-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-circle-left</div>
        </li>
        <li>
            <i class="fas fa-chevron-circle-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-circle-right</div>
        </li>
        <li>
            <i class="fas fa-chevron-circle-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-circle-up</div>
        </li>
        <li>
            <i class="fas fa-chevron-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-down</div>
        </li>
        <li>
            <i class="fas fa-chevron-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-left</div>
        </li>
        <li>
            <i class="fas fa-chevron-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-right</div>
        </li>
        <li>
            <i class="fas fa-chevron-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-chevron-up</div>
        </li>
        <li>
            <i class="fas fa-child fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-child</div>
        </li>
        <li>
            <i class="fas fa-church fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-church</div>
        </li>
        <li>
            <i class="fas fa-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-circle</div>
        </li>
        <li>
            <i class="fas fa-circle-notch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-circle-notch</div>
        </li>
        <li>
            <i class="fas fa-city fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-city</div>
        </li>
        <li>
            <i class="fas fa-clinic-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clinic-medical</div>
        </li>
        <li>
            <i class="fas fa-clipboard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clipboard</div>
        </li>
        <li>
            <i class="fas fa-clipboard-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clipboard-check</div>
        </li>
        <li>
            <i class="fas fa-clipboard-list fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clipboard-list</div>
        </li>
        <li>
            <i class="fas fa-clock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clock</div>
        </li>
        <li>
            <i class="fas fa-clone fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-clone</div>
        </li>
        <li>
            <i class="fas fa-closed-captioning fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-closed-captioning</div>
        </li>
        <li>
            <i class="fas fa-cloud fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud</div>
        </li>
        <li>
            <i class="fas fa-cloud-download-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-download-alt</div>
        </li>
        <li>
            <i class="fas fa-cloud-meatball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-meatball</div>
        </li>
        <li>
            <i class="fas fa-cloud-moon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-moon</div>
        </li>
        <li>
            <i class="fas fa-cloud-moon-rain fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-moon-rain</div>
        </li>
        <li>
            <i class="fas fa-cloud-rain fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-rain</div>
        </li>
        <li>
            <i class="fas fa-cloud-showers-heavy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-showers-heavy</div>
        </li>
        <li>
            <i class="fas fa-cloud-sun fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-sun</div>
        </li>
        <li>
            <i class="fas fa-cloud-sun-rain fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-sun-rain</div>
        </li>
        <li>
            <i class="fas fa-cloud-upload-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cloud-upload-alt</div>
        </li>
        <li>
            <i class="fas fa-cocktail fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cocktail</div>
        </li>
        <li>
            <i class="fas fa-code fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-code</div>
        </li>
        <li>
            <i class="fas fa-code-branch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-code-branch</div>
        </li>
        <li>
            <i class="fas fa-coffee fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-coffee</div>
        </li>
        <li>
            <i class="fas fa-cog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cog</div>
        </li>
        <li>
            <i class="fas fa-cogs fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cogs</div>
        </li>
        <li>
            <i class="fas fa-coins fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-coins</div>
        </li>
        <li>
            <i class="fas fa-columns fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-columns</div>
        </li>
        <li>
            <i class="fas fa-comment fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment</div>
        </li>
        <li>
            <i class="fas fa-comment-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment-alt</div>
        </li>
        <li>
            <i class="fas fa-comment-dollar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment-dollar</div>
        </li>
        <li>
            <i class="fas fa-comment-dots fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment-dots</div>
        </li>
        <li>
            <i class="fas fa-comment-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment-medical</div>
        </li>
        <li>
            <i class="fas fa-comment-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comment-slash</div>
        </li>
        <li>
            <i class="fas fa-comments fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comments</div>
        </li>
        <li>
            <i class="fas fa-comments-dollar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-comments-dollar</div>
        </li>
        <li>
            <i class="fas fa-compact-disc fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-compact-disc</div>
        </li>
        <li>
            <i class="fas fa-compass fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-compass</div>
        </li>
        <li>
            <i class="fas fa-compress fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-compress</div>
        </li>
        <li>
            <i class="fas fa-compress-arrows-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-compress-arrows-alt</div>
        </li>
        <li>
            <i class="fas fa-concierge-bell fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-concierge-bell</div>
        </li>
        <li>
            <i class="fas fa-cookie fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cookie</div>
        </li>
        <li>
            <i class="fas fa-cookie-bite fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cookie-bite</div>
        </li>
        <li>
            <i class="fas fa-copy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-copy</div>
        </li>
        <li>
            <i class="fas fa-copyright fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-copyright</div>
        </li>
        <li>
            <i class="fas fa-couch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-couch</div>
        </li>
        <li>
            <i class="fas fa-credit-card fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-credit-card</div>
        </li>
        <li>
            <i class="fas fa-crop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crop</div>
        </li>
        <li>
            <i class="fas fa-crop-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crop-alt</div>
        </li>
        <li>
            <i class="fas fa-cross fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cross</div>
        </li>
        <li>
            <i class="fas fa-crosshairs fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crosshairs</div>
        </li>
        <li>
            <i class="fas fa-crow fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crow</div>
        </li>
        <li>
            <i class="fas fa-crown fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crown</div>
        </li>
        <li>
            <i class="fas fa-crutch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-crutch</div>
        </li>
        <li>
            <i class="fas fa-cube fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cube</div>
        </li>
        <li>
            <i class="fas fa-cubes fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cubes</div>
        </li>
        <li>
            <i class="fas fa-cut fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-cut</div>
        </li>
        <li>
            <i class="fas fa-database fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-database</div>
        </li>
        <li>
            <i class="fas fa-deaf fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-deaf</div>
        </li>
        <li>
            <i class="fas fa-democrat fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-democrat</div>
        </li>
        <li>
            <i class="fas fa-desktop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-desktop</div>
        </li>
        <li>
            <i class="fas fa-dharmachakra fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dharmachakra</div>
        </li>
        <li>
            <i class="fas fa-diagnoses fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-diagnoses</div>
        </li>
        <li>
            <i class="fas fa-dice fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice</div>
        </li>
        <li>
            <i class="fas fa-dice-d20 fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-d20</div>
        </li>
        <li>
            <i class="fas fa-dice-d6 fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-d6</div>
        </li>
        <li>
            <i class="fas fa-dice-five fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-five</div>
        </li>
        <li>
            <i class="fas fa-dice-four fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-four</div>
        </li>
        <li>
            <i class="fas fa-dice-one fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-one</div>
        </li>
        <li>
            <i class="fas fa-dice-six fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-six</div>
        </li>
        <li>
            <i class="fas fa-dice-three fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-three</div>
        </li>
        <li>
            <i class="fas fa-dice-two fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dice-two</div>
        </li>
        <li>
            <i class="fas fa-digital-tachograph fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-digital-tachograph</div>
        </li>
        <li>
            <i class="fas fa-directions fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-directions</div>
        </li>
        <li>
            <i class="fas fa-divide fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-divide</div>
        </li>
        <li>
            <i class="fas fa-dizzy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dizzy</div>
        </li>
        <li>
            <i class="fas fa-dna fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dna</div>
        </li>
        <li>
            <i class="fas fa-dog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dog</div>
        </li>
        <li>
            <i class="fas fa-dollar-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dollar-sign</div>
        </li>
        <li>
            <i class="fas fa-dolly fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dolly</div>
        </li>
        <li>
            <i class="fas fa-dolly-flatbed fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dolly-flatbed</div>
        </li>
        <li>
            <i class="fas fa-donate fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-donate</div>
        </li>
        <li>
            <i class="fas fa-door-closed fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-door-closed</div>
        </li>
        <li>
            <i class="fas fa-door-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-door-open</div>
        </li>
        <li>
            <i class="fas fa-dot-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dot-circle</div>
        </li>
        <li>
            <i class="fas fa-dove fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dove</div>
        </li>
        <li>
            <i class="fas fa-download fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-download</div>
        </li>
        <li>
            <i class="fas fa-drafting-compass fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-drafting-compass</div>
        </li>
        <li>
            <i class="fas fa-dragon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dragon</div>
        </li>
        <li>
            <i class="fas fa-draw-polygon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-draw-polygon</div>
        </li>
        <li>
            <i class="fas fa-drum fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-drum</div>
        </li>
        <li>
            <i class="fas fa-drum-steelpan fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-drum-steelpan</div>
        </li>
        <li>
            <i class="fas fa-drumstick-bite fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-drumstick-bite</div>
        </li>
        <li>
            <i class="fas fa-dumbbell fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dumbbell</div>
        </li>
        <li>
            <i class="fas fa-dumpster fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dumpster</div>
        </li>
        <li>
            <i class="fas fa-dumpster-fire fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dumpster-fire</div>
        </li>
        <li>
            <i class="fas fa-dungeon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-dungeon</div>
        </li>
        <li>
            <i class="fas fa-edit fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-edit</div>
        </li>
        <li>
            <i class="fas fa-egg fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-egg</div>
        </li>
        <li>
            <i class="fas fa-eject fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-eject</div>
        </li>
        <li>
            <i class="fas fa-ellipsis-h fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ellipsis-h</div>
        </li>
        <li>
            <i class="fas fa-ellipsis-v fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ellipsis-v</div>
        </li>
        <li>
            <i class="fas fa-envelope fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-envelope</div>
        </li>
        <li>
            <i class="fas fa-envelope-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-envelope-open</div>
        </li>
        <li>
            <i class="fas fa-envelope-open-text fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-envelope-open-text</div>
        </li>
        <li>
            <i class="fas fa-envelope-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-envelope-square</div>
        </li>
        <li>
            <i class="fas fa-equals fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-equals</div>
        </li>
        <li>
            <i class="fas fa-eraser fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-eraser</div>
        </li>
        <li>
            <i class="fas fa-ethernet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ethernet</div>
        </li>
        <li>
            <i class="fas fa-euro-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-euro-sign</div>
        </li>
        <li>
            <i class="fas fa-exchange-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-exchange-alt</div>
        </li>
        <li>
            <i class="fas fa-exclamation fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-exclamation</div>
        </li>
        <li>
            <i class="fas fa-exclamation-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-exclamation-circle</div>
        </li>
        <li>
            <i class="fas fa-exclamation-triangle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-exclamation-triangle</div>
        </li>
        <li>
            <i class="fas fa-expand fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-expand</div>
        </li>
        <li>
            <i class="fas fa-expand-arrows-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-expand-arrows-alt</div>
        </li>
        <li>
            <i class="fas fa-external-link-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-external-link-alt</div>
        </li>
        <li>
            <i class="fas fa-external-link-square-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-external-link-square-alt</div>
        </li>
        <li>
            <i class="fas fa-eye fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-eye</div>
        </li>
        <li>
            <i class="fas fa-eye-dropper fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-eye-dropper</div>
        </li>
        <li>
            <i class="fas fa-eye-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-eye-slash</div>
        </li>
        <li>
            <i class="fas fa-fan fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fan</div>
        </li>
        <li>
            <i class="fas fa-fast-backward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fast-backward</div>
        </li>
        <li>
            <i class="fas fa-fast-forward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fast-forward</div>
        </li>
        <li>
            <i class="fas fa-fax fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fax</div>
        </li>
        <li>
            <i class="fas fa-feather fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-feather</div>
        </li>
        <li>
            <i class="fas fa-feather-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-feather-alt</div>
        </li>
        <li>
            <i class="fas fa-female fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-female</div>
        </li>
        <li>
            <i class="fas fa-fighter-jet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fighter-jet</div>
        </li>
        <li>
            <i class="fas fa-file fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file</div>
        </li>
        <li>
            <i class="fas fa-file-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-alt</div>
        </li>
        <li>
            <i class="fas fa-file-archive fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-archive</div>
        </li>
        <li>
            <i class="fas fa-file-audio fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-audio</div>
        </li>
        <li>
            <i class="fas fa-file-code fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-code</div>
        </li>
        <li>
            <i class="fas fa-file-contract fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-contract</div>
        </li>
        <li>
            <i class="fas fa-file-csv fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-csv</div>
        </li>
        <li>
            <i class="fas fa-file-download fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-download</div>
        </li>
        <li>
            <i class="fas fa-file-excel fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-excel</div>
        </li>
        <li>
            <i class="fas fa-file-export fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-export</div>
        </li>
        <li>
            <i class="fas fa-file-image fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-image</div>
        </li>
        <li>
            <i class="fas fa-file-import fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-import</div>
        </li>
        <li>
            <i class="fas fa-file-invoice fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-invoice</div>
        </li>
        <li>
            <i class="fas fa-file-invoice-dollar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-invoice-dollar</div>
        </li>
        <li>
            <i class="fas fa-file-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-medical</div>
        </li>
        <li>
            <i class="fas fa-file-medical-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-medical-alt</div>
        </li>
        <li>
            <i class="fas fa-file-pdf fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-pdf</div>
        </li>
        <li>
            <i class="fas fa-file-powerpoint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-powerpoint</div>
        </li>
        <li>
            <i class="fas fa-file-prescription fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-prescription</div>
        </li>
        <li>
            <i class="fas fa-file-signature fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-signature</div>
        </li>
        <li>
            <i class="fas fa-file-upload fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-upload</div>
        </li>
        <li>
            <i class="fas fa-file-video fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-video</div>
        </li>
        <li>
            <i class="fas fa-file-word fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-file-word</div>
        </li>
        <li>
            <i class="fas fa-fill fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fill</div>
        </li>
        <li>
            <i class="fas fa-fill-drip fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fill-drip</div>
        </li>
        <li>
            <i class="fas fa-film fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-film</div>
        </li>
        <li>
            <i class="fas fa-filter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-filter</div>
        </li>
        <li>
            <i class="fas fa-fingerprint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fingerprint</div>
        </li>
        <li>
            <i class="fas fa-fire fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fire</div>
        </li>
        <li>
            <i class="fas fa-fire-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fire-alt</div>
        </li>
        <li>
            <i class="fas fa-fire-extinguisher fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fire-extinguisher</div>
        </li>
        <li>
            <i class="fas fa-first-aid fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-first-aid</div>
        </li>
        <li>
            <i class="fas fa-fish fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fish</div>
        </li>
        <li>
            <i class="fas fa-fist-raised fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-fist-raised</div>
        </li>
        <li>
            <i class="fas fa-flag fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-flag</div>
        </li>
        <li>
            <i class="fas fa-flag-checkered fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-flag-checkered</div>
        </li>
        <li>
            <i class="fas fa-flag-usa fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-flag-usa</div>
        </li>
        <li>
            <i class="fas fa-flask fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-flask</div>
        </li>
        <li>
            <i class="fas fa-flushed fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-flushed</div>
        </li>
        <li>
            <i class="fas fa-folder fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-folder</div>
        </li>
        <li>
            <i class="fas fa-folder-minus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-folder-minus</div>
        </li>
        <li>
            <i class="fas fa-folder-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-folder-open</div>
        </li>
        <li>
            <i class="fas fa-folder-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-folder-plus</div>
        </li>
        <li>
            <i class="fas fa-font fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-font</div>
        </li>
        <li>
            <i class="fas fa-football-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-football-ball</div>
        </li>
        <li>
            <i class="fas fa-forward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-forward</div>
        </li>
        <li>
            <i class="fas fa-frog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-frog</div>
        </li>
        <li>
            <i class="fas fa-frown fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-frown</div>
        </li>
        <li>
            <i class="fas fa-frown-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-frown-open</div>
        </li>
        <li>
            <i class="fas fa-funnel-dollar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-funnel-dollar</div>
        </li>
        <li>
            <i class="fas fa-futbol fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-futbol</div>
        </li>
        <li>
            <i class="fas fa-gamepad fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gamepad</div>
        </li>
        <li>
            <i class="fas fa-gas-pump fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gas-pump</div>
        </li>
        <li>
            <i class="fas fa-gavel fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gavel</div>
        </li>
        <li>
            <i class="fas fa-gem fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gem</div>
        </li>
        <li>
            <i class="fas fa-genderless fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-genderless</div>
        </li>
        <li>
            <i class="fas fa-ghost fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ghost</div>
        </li>
        <li>
            <i class="fas fa-gift fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gift</div>
        </li>
        <li>
            <i class="fas fa-gifts fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gifts</div>
        </li>
        <li>
            <i class="fas fa-glass-cheers fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-glass-cheers</div>
        </li>
        <li>
            <i class="fas fa-glass-martini fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-glass-martini</div>
        </li>
        <li>
            <i class="fas fa-glass-martini-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-glass-martini-alt</div>
        </li>
        <li>
            <i class="fas fa-glass-whiskey fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-glass-whiskey</div>
        </li>
        <li>
            <i class="fas fa-glasses fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-glasses</div>
        </li>
        <li>
            <i class="fas fa-globe fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-globe</div>
        </li>
        <li>
            <i class="fas fa-globe-africa fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-globe-africa</div>
        </li>
        <li>
            <i class="fas fa-globe-americas fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-globe-americas</div>
        </li>
        <li>
            <i class="fas fa-globe-asia fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-globe-asia</div>
        </li>
        <li>
            <i class="fas fa-globe-europe fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-globe-europe</div>
        </li>
        <li>
            <i class="fas fa-golf-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-golf-ball</div>
        </li>
        <li>
            <i class="fas fa-gopuram fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-gopuram</div>
        </li>
        <li>
            <i class="fas fa-graduation-cap fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-graduation-cap</div>
        </li>
        <li>
            <i class="fas fa-greater-than fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-greater-than</div>
        </li>
        <li>
            <i class="fas fa-greater-than-equal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-greater-than-equal</div>
        </li>
        <li>
            <i class="fas fa-grimace fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grimace</div>
        </li>
        <li>
            <i class="fas fa-grin fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin</div>
        </li>
        <li>
            <i class="fas fa-grin-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-alt</div>
        </li>
        <li>
            <i class="fas fa-grin-beam fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-beam</div>
        </li>
        <li>
            <i class="fas fa-grin-beam-sweat fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-beam-sweat</div>
        </li>
        <li>
            <i class="fas fa-grin-hearts fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-hearts</div>
        </li>
        <li>
            <i class="fas fa-grin-squint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-squint</div>
        </li>
        <li>
            <i class="fas fa-grin-squint-tears fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-squint-tears</div>
        </li>
        <li>
            <i class="fas fa-grin-stars fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-stars</div>
        </li>
        <li>
            <i class="fas fa-grin-tears fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-tears</div>
        </li>
        <li>
            <i class="fas fa-grin-tongue fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-tongue</div>
        </li>
        <li>
            <i class="fas fa-grin-tongue-squint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-tongue-squint</div>
        </li>
        <li>
            <i class="fas fa-grin-tongue-wink fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-tongue-wink</div>
        </li>
        <li>
            <i class="fas fa-grin-wink fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grin-wink</div>
        </li>
        <li>
            <i class="fas fa-grip-horizontal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grip-horizontal</div>
        </li>
        <li>
            <i class="fas fa-grip-lines fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grip-lines</div>
        </li>
        <li>
            <i class="fas fa-grip-lines-vertical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grip-lines-vertical</div>
        </li>
        <li>
            <i class="fas fa-grip-vertical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-grip-vertical</div>
        </li>
        <li>
            <i class="fas fa-guitar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-guitar</div>
        </li>
        <li>
            <i class="fas fa-h-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-h-square</div>
        </li>
        <li>
            <i class="fas fa-hamburger fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hamburger</div>
        </li>
        <li>
            <i class="fas fa-hammer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hammer</div>
        </li>
        <li>
            <i class="fas fa-hamsa fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hamsa</div>
        </li>
        <li>
            <i class="fas fa-hand-holding fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-holding</div>
        </li>
        <li>
            <i class="fas fa-hand-holding-heart fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-holding-heart</div>
        </li>
        <li>
            <i class="fas fa-hand-holding-usd fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-holding-usd</div>
        </li>
        <li>
            <i class="fas fa-hand-lizard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-lizard</div>
        </li>
        <li>
            <i class="fas fa-hand-middle-finger fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-middle-finger</div>
        </li>
        <li>
            <i class="fas fa-hand-paper fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-paper</div>
        </li>
        <li>
            <i class="fas fa-hand-peace fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-peace</div>
        </li>
        <li>
            <i class="fas fa-hand-point-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-point-down</div>
        </li>
        <li>
            <i class="fas fa-hand-point-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-point-left</div>
        </li>
        <li>
            <i class="fas fa-hand-point-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-point-right</div>
        </li>
        <li>
            <i class="fas fa-hand-point-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-point-up</div>
        </li>
        <li>
            <i class="fas fa-hand-pointer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-pointer</div>
        </li>
        <li>
            <i class="fas fa-hand-rock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-rock</div>
        </li>
        <li>
            <i class="fas fa-hand-scissors fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-scissors</div>
        </li>
        <li>
            <i class="fas fa-hand-spock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hand-spock</div>
        </li>
        <li>
            <i class="fas fa-hands fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hands</div>
        </li>
        <li>
            <i class="fas fa-hands-helping fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hands-helping</div>
        </li>
        <li>
            <i class="fas fa-handshake fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-handshake</div>
        </li>
        <li>
            <i class="fas fa-hanukiah fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hanukiah</div>
        </li>
        <li>
            <i class="fas fa-hard-hat fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hard-hat</div>
        </li>
        <li>
            <i class="fas fa-hashtag fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hashtag</div>
        </li>
        <li>
            <i class="fas fa-hat-cowboy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hat-cowboy</div>
        </li>
        <li>
            <i class="fas fa-hat-cowboy-side fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hat-cowboy-side</div>
        </li>
        <li>
            <i class="fas fa-hat-wizard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hat-wizard</div>
        </li>
        <li>
            <i class="fas fa-haykal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-haykal</div>
        </li>
        <li>
            <i class="fas fa-hdd fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hdd</div>
        </li>
        <li>
            <i class="fas fa-heading fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-heading</div>
        </li>
        <li>
            <i class="fas fa-headphones fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-headphones</div>
        </li>
        <li>
            <i class="fas fa-headphones-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-headphones-alt</div>
        </li>
        <li>
            <i class="fas fa-headset fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-headset</div>
        </li>
        <li>
            <i class="fas fa-heart fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-heart</div>
        </li>
        <li>
            <i class="fas fa-heart-broken fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-heart-broken</div>
        </li>
        <li>
            <i class="fas fa-heartbeat fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-heartbeat</div>
        </li>
        <li>
            <i class="fas fa-helicopter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-helicopter</div>
        </li>
        <li>
            <i class="fas fa-highlighter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-highlighter</div>
        </li>
        <li>
            <i class="fas fa-hiking fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hiking</div>
        </li>
        <li>
            <i class="fas fa-hippo fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hippo</div>
        </li>
        <li>
            <i class="fas fa-history fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-history</div>
        </li>
        <li>
            <i class="fas fa-hockey-puck fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hockey-puck</div>
        </li>
        <li>
            <i class="fas fa-holly-berry fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-holly-berry</div>
        </li>
        <li>
            <i class="fas fa-home fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-home</div>
        </li>
        <li>
            <i class="fas fa-horse fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-horse</div>
        </li>
        <li>
            <i class="fas fa-horse-head fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-horse-head</div>
        </li>
        <li>
            <i class="fas fa-hospital fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hospital</div>
        </li>
        <li>
            <i class="fas fa-hospital-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hospital-alt</div>
        </li>
        <li>
            <i class="fas fa-hospital-symbol fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hospital-symbol</div>
        </li>
        <li>
            <i class="fas fa-hot-tub fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hot-tub</div>
        </li>
        <li>
            <i class="fas fa-hotdog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hotdog</div>
        </li>
        <li>
            <i class="fas fa-hotel fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hotel</div>
        </li>
        <li>
            <i class="fas fa-hourglass fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hourglass</div>
        </li>
        <li>
            <i class="fas fa-hourglass-end fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hourglass-end</div>
        </li>
        <li>
            <i class="fas fa-hourglass-half fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hourglass-half</div>
        </li>
        <li>
            <i class="fas fa-hourglass-start fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hourglass-start</div>
        </li>
        <li>
            <i class="fas fa-house-damage fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-house-damage</div>
        </li>
        <li>
            <i class="fas fa-hryvnia fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-hryvnia</div>
        </li>
        <li>
            <i class="fas fa-i-cursor fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-i-cursor</div>
        </li>
        <li>
            <i class="fas fa-ice-cream fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ice-cream</div>
        </li>
        <li>
            <i class="fas fa-icicles fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-icicles</div>
        </li>
        <li>
            <i class="fas fa-icons fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-icons</div>
        </li>
        <li>
            <i class="fas fa-id-badge fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-id-badge</div>
        </li>
        <li>
            <i class="fas fa-id-card fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-id-card</div>
        </li>
        <li>
            <i class="fas fa-id-card-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-id-card-alt</div>
        </li>
        <li>
            <i class="fas fa-igloo fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-igloo</div>
        </li>
        <li>
            <i class="fas fa-image fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-image</div>
        </li>
        <li>
            <i class="fas fa-images fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-images</div>
        </li>
        <li>
            <i class="fas fa-inbox fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-inbox</div>
        </li>
        <li>
            <i class="fas fa-indent fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-indent</div>
        </li>
        <li>
            <i class="fas fa-industry fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-industry</div>
        </li>
        <li>
            <i class="fas fa-infinity fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-infinity</div>
        </li>
        <li>
            <i class="fas fa-info fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-info</div>
        </li>
        <li>
            <i class="fas fa-info-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-info-circle</div>
        </li>
        <li>
            <i class="fas fa-italic fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-italic</div>
        </li>
        <li>
            <i class="fas fa-jedi fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-jedi</div>
        </li>
        <li>
            <i class="fas fa-joint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-joint</div>
        </li>
        <li>
            <i class="fas fa-journal-whills fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-journal-whills</div>
        </li>
        <li>
            <i class="fas fa-kaaba fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-kaaba</div>
        </li>
        <li>
            <i class="fas fa-key fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-key</div>
        </li>
        <li>
            <i class="fas fa-keyboard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-keyboard</div>
        </li>
        <li>
            <i class="fas fa-khanda fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-khanda</div>
        </li>
        <li>
            <i class="fas fa-kiss fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-kiss</div>
        </li>
        <li>
            <i class="fas fa-kiss-beam fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-kiss-beam</div>
        </li>
        <li>
            <i class="fas fa-kiss-wink-heart fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-kiss-wink-heart</div>
        </li>
        <li>
            <i class="fas fa-kiwi-bird fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-kiwi-bird</div>
        </li>
        <li>
            <i class="fas fa-landmark fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-landmark</div>
        </li>
        <li>
            <i class="fas fa-language fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-language</div>
        </li>
        <li>
            <i class="fas fa-laptop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laptop</div>
        </li>
        <li>
            <i class="fas fa-laptop-code fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laptop-code</div>
        </li>
        <li>
            <i class="fas fa-laptop-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laptop-medical</div>
        </li>
        <li>
            <i class="fas fa-laugh fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laugh</div>
        </li>
        <li>
            <i class="fas fa-laugh-beam fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laugh-beam</div>
        </li>
        <li>
            <i class="fas fa-laugh-squint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laugh-squint</div>
        </li>
        <li>
            <i class="fas fa-laugh-wink fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-laugh-wink</div>
        </li>
        <li>
            <i class="fas fa-layer-group fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-layer-group</div>
        </li>
        <li>
            <i class="fas fa-leaf fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-leaf</div>
        </li>
        <li>
            <i class="fas fa-lemon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-lemon</div>
        </li>
        <li>
            <i class="fas fa-less-than fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-less-than</div>
        </li>
        <li>
            <i class="fas fa-less-than-equal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-less-than-equal</div>
        </li>
        <li>
            <i class="fas fa-level-down-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-level-down-alt</div>
        </li>
        <li>
            <i class="fas fa-level-up-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-level-up-alt</div>
        </li>
        <li>
            <i class="fas fa-life-ring fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-life-ring</div>
        </li>
        <li>
            <i class="fas fa-lightbulb fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-lightbulb</div>
        </li>
        <li>
            <i class="fas fa-link fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-link</div>
        </li>
        <li>
            <i class="fas fa-lira-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-lira-sign</div>
        </li>
        <li>
            <i class="fas fa-list fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-list</div>
        </li>
        <li>
            <i class="fas fa-list-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-list-alt</div>
        </li>
        <li>
            <i class="fas fa-list-ol fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-list-ol</div>
        </li>
        <li>
            <i class="fas fa-list-ul fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-list-ul</div>
        </li>
        <li>
            <i class="fas fa-location-arrow fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-location-arrow</div>
        </li>
        <li>
            <i class="fas fa-lock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-lock</div>
        </li>
        <li>
            <i class="fas fa-lock-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-lock-open</div>
        </li>
        <li>
            <i class="fas fa-long-arrow-alt-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-long-arrow-alt-down</div>
        </li>
        <li>
            <i class="fas fa-long-arrow-alt-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-long-arrow-alt-left</div>
        </li>
        <li>
            <i class="fas fa-long-arrow-alt-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-long-arrow-alt-right</div>
        </li>
        <li>
            <i class="fas fa-long-arrow-alt-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-long-arrow-alt-up</div>
        </li>
        <li>
            <i class="fas fa-low-vision fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-low-vision</div>
        </li>
        <li>
            <i class="fas fa-luggage-cart fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-luggage-cart</div>
        </li>
        <li>
            <i class="fas fa-magic fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-magic</div>
        </li>
        <li>
            <i class="fas fa-magnet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-magnet</div>
        </li>
        <li>
            <i class="fas fa-mail-bulk fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mail-bulk</div>
        </li>
        <li>
            <i class="fas fa-male fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-male</div>
        </li>
        <li>
            <i class="fas fa-map fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map</div>
        </li>
        <li>
            <i class="fas fa-map-marked fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-marked</div>
        </li>
        <li>
            <i class="fas fa-map-marked-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-marked-alt</div>
        </li>
        <li>
            <i class="fas fa-map-marker fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-marker</div>
        </li>
        <li>
            <i class="fas fa-map-marker-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-marker-alt</div>
        </li>
        <li>
            <i class="fas fa-map-pin fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-pin</div>
        </li>
        <li>
            <i class="fas fa-map-signs fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-map-signs</div>
        </li>
        <li>
            <i class="fas fa-marker fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-marker</div>
        </li>
        <li>
            <i class="fas fa-mars fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mars</div>
        </li>
        <li>
            <i class="fas fa-mars-double fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mars-double</div>
        </li>
        <li>
            <i class="fas fa-mars-stroke fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mars-stroke</div>
        </li>
        <li>
            <i class="fas fa-mars-stroke-h fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mars-stroke-h</div>
        </li>
        <li>
            <i class="fas fa-mars-stroke-v fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mars-stroke-v</div>
        </li>
        <li>
            <i class="fas fa-mask fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mask</div>
        </li>
        <li>
            <i class="fas fa-medal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-medal</div>
        </li>
        <li>
            <i class="fas fa-medkit fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-medkit</div>
        </li>
        <li>
            <i class="fas fa-meh fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-meh</div>
        </li>
        <li>
            <i class="fas fa-meh-blank fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-meh-blank</div>
        </li>
        <li>
            <i class="fas fa-meh-rolling-eyes fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-meh-rolling-eyes</div>
        </li>
        <li>
            <i class="fas fa-memory fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-memory</div>
        </li>
        <li>
            <i class="fas fa-menorah fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-menorah</div>
        </li>
        <li>
            <i class="fas fa-mercury fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mercury</div>
        </li>
        <li>
            <i class="fas fa-meteor fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-meteor</div>
        </li>
        <li>
            <i class="fas fa-microchip fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microchip</div>
        </li>
        <li>
            <i class="fas fa-microphone fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microphone</div>
        </li>
        <li>
            <i class="fas fa-microphone-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microphone-alt</div>
        </li>
        <li>
            <i class="fas fa-microphone-alt-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microphone-alt-slash</div>
        </li>
        <li>
            <i class="fas fa-microphone-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microphone-slash</div>
        </li>
        <li>
            <i class="fas fa-microscope fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-microscope</div>
        </li>
        <li>
            <i class="fas fa-minus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-minus</div>
        </li>
        <li>
            <i class="fas fa-minus-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-minus-circle</div>
        </li>
        <li>
            <i class="fas fa-minus-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-minus-square</div>
        </li>
        <li>
            <i class="fas fa-mitten fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mitten</div>
        </li>
        <li>
            <i class="fas fa-mobile fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mobile</div>
        </li>
        <li>
            <i class="fas fa-mobile-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mobile-alt</div>
        </li>
        <li>
            <i class="fas fa-money-bill fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-bill</div>
        </li>
        <li>
            <i class="fas fa-money-bill-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-bill-alt</div>
        </li>
        <li>
            <i class="fas fa-money-bill-wave fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-bill-wave</div>
        </li>
        <li>
            <i class="fas fa-money-bill-wave-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-bill-wave-alt</div>
        </li>
        <li>
            <i class="fas fa-money-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-check</div>
        </li>
        <li>
            <i class="fas fa-money-check-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-money-check-alt</div>
        </li>
        <li>
            <i class="fas fa-monument fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-monument</div>
        </li>
        <li>
            <i class="fas fa-moon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-moon</div>
        </li>
        <li>
            <i class="fas fa-mortar-pestle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mortar-pestle</div>
        </li>
        <li>
            <i class="fas fa-mosque fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mosque</div>
        </li>
        <li>
            <i class="fas fa-motorcycle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-motorcycle</div>
        </li>
        <li>
            <i class="fas fa-mountain fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mountain</div>
        </li>
        <li>
            <i class="fas fa-mouse fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mouse</div>
        </li>
        <li>
            <i class="fas fa-mouse-pointer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mouse-pointer</div>
        </li>
        <li>
            <i class="fas fa-mug-hot fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-mug-hot</div>
        </li>
        <li>
            <i class="fas fa-music fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-music</div>
        </li>
        <li>
            <i class="fas fa-network-wired fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-network-wired</div>
        </li>
        <li>
            <i class="fas fa-neuter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-neuter</div>
        </li>
        <li>
            <i class="fas fa-newspaper fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-newspaper</div>
        </li>
        <li>
            <i class="fas fa-not-equal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-not-equal</div>
        </li>
        <li>
            <i class="fas fa-notes-medical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-notes-medical</div>
        </li>
        <li>
            <i class="fas fa-object-group fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-object-group</div>
        </li>
        <li>
            <i class="fas fa-object-ungroup fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-object-ungroup</div>
        </li>
        <li>
            <i class="fas fa-oil-can fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-oil-can</div>
        </li>
        <li>
            <i class="fas fa-om fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-om</div>
        </li>
        <li>
            <i class="fas fa-otter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-otter</div>
        </li>
        <li>
            <i class="fas fa-outdent fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-outdent</div>
        </li>
        <li>
            <i class="fas fa-pager fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pager</div>
        </li>
        <li>
            <i class="fas fa-paint-brush fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paint-brush</div>
        </li>
        <li>
            <i class="fas fa-paint-roller fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paint-roller</div>
        </li>
        <li>
            <i class="fas fa-palette fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-palette</div>
        </li>
        <li>
            <i class="fas fa-pallet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pallet</div>
        </li>
        <li>
            <i class="fas fa-paper-plane fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paper-plane</div>
        </li>
        <li>
            <i class="fas fa-paperclip fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paperclip</div>
        </li>
        <li>
            <i class="fas fa-parachute-box fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-parachute-box</div>
        </li>
        <li>
            <i class="fas fa-paragraph fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paragraph</div>
        </li>
        <li>
            <i class="fas fa-parking fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-parking</div>
        </li>
        <li>
            <i class="fas fa-passport fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-passport</div>
        </li>
        <li>
            <i class="fas fa-pastafarianism fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pastafarianism</div>
        </li>
        <li>
            <i class="fas fa-paste fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paste</div>
        </li>
        <li>
            <i class="fas fa-pause fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pause</div>
        </li>
        <li>
            <i class="fas fa-pause-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pause-circle</div>
        </li>
        <li>
            <i class="fas fa-paw fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-paw</div>
        </li>
        <li>
            <i class="fas fa-peace fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-peace</div>
        </li>
        <li>
            <i class="fas fa-pen fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pen</div>
        </li>
        <li>
            <i class="fas fa-pen-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pen-alt</div>
        </li>
        <li>
            <i class="fas fa-pen-fancy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pen-fancy</div>
        </li>
        <li>
            <i class="fas fa-pen-nib fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pen-nib</div>
        </li>
        <li>
            <i class="fas fa-pen-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pen-square</div>
        </li>
        <li>
            <i class="fas fa-pencil-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pencil-alt</div>
        </li>
        <li>
            <i class="fas fa-pencil-ruler fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pencil-ruler</div>
        </li>
        <li>
            <i class="fas fa-people-carry fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-people-carry</div>
        </li>
        <li>
            <i class="fas fa-pepper-hot fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pepper-hot</div>
        </li>
        <li>
            <i class="fas fa-percent fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-percent</div>
        </li>
        <li>
            <i class="fas fa-percentage fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-percentage</div>
        </li>
        <li>
            <i class="fas fa-person-booth fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-person-booth</div>
        </li>
        <li>
            <i class="fas fa-phone fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone</div>
        </li>
        <li>
            <i class="fas fa-phone-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone-alt</div>
        </li>
        <li>
            <i class="fas fa-phone-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone-slash</div>
        </li>
        <li>
            <i class="fas fa-phone-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone-square</div>
        </li>
        <li>
            <i class="fas fa-phone-square-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone-square-alt</div>
        </li>
        <li>
            <i class="fas fa-phone-volume fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-phone-volume</div>
        </li>
        <li>
            <i class="fas fa-photo-video fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-photo-video</div>
        </li>
        <li>
            <i class="fas fa-piggy-bank fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-piggy-bank</div>
        </li>
        <li>
            <i class="fas fa-pills fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pills</div>
        </li>
        <li>
            <i class="fas fa-pizza-slice fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pizza-slice</div>
        </li>
        <li>
            <i class="fas fa-place-of-worship fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-place-of-worship</div>
        </li>
        <li>
            <i class="fas fa-plane fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plane</div>
        </li>
        <li>
            <i class="fas fa-plane-arrival fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plane-arrival</div>
        </li>
        <li>
            <i class="fas fa-plane-departure fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plane-departure</div>
        </li>
        <li>
            <i class="fas fa-play fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-play</div>
        </li>
        <li>
            <i class="fas fa-play-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-play-circle</div>
        </li>
        <li>
            <i class="fas fa-plug fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plug</div>
        </li>
        <li>
            <i class="fas fa-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plus</div>
        </li>
        <li>
            <i class="fas fa-plus-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plus-circle</div>
        </li>
        <li>
            <i class="fas fa-plus-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-plus-square</div>
        </li>
        <li>
            <i class="fas fa-podcast fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-podcast</div>
        </li>
        <li>
            <i class="fas fa-poll fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-poll</div>
        </li>
        <li>
            <i class="fas fa-poll-h fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-poll-h</div>
        </li>
        <li>
            <i class="fas fa-poo fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-poo</div>
        </li>
        <li>
            <i class="fas fa-poo-storm fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-poo-storm</div>
        </li>
        <li>
            <i class="fas fa-poop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-poop</div>
        </li>
        <li>
            <i class="fas fa-portrait fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-portrait</div>
        </li>
        <li>
            <i class="fas fa-pound-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pound-sign</div>
        </li>
        <li>
            <i class="fas fa-power-off fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-power-off</div>
        </li>
        <li>
            <i class="fas fa-pray fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-pray</div>
        </li>
        <li>
            <i class="fas fa-praying-hands fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-praying-hands</div>
        </li>
        <li>
            <i class="fas fa-prescription fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-prescription</div>
        </li>
        <li>
            <i class="fas fa-prescription-bottle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-prescription-bottle</div>
        </li>
        <li>
            <i class="fas fa-prescription-bottle-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-prescription-bottle-alt</div>
        </li>
        <li>
            <i class="fas fa-print fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-print</div>
        </li>
        <li>
            <i class="fas fa-procedures fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-procedures</div>
        </li>
        <li>
            <i class="fas fa-project-diagram fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-project-diagram</div>
        </li>
        <li>
            <i class="fas fa-puzzle-piece fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-puzzle-piece</div>
        </li>
        <li>
            <i class="fas fa-qrcode fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-qrcode</div>
        </li>
        <li>
            <i class="fas fa-question fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-question</div>
        </li>
        <li>
            <i class="fas fa-question-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-question-circle</div>
        </li>
        <li>
            <i class="fas fa-quidditch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-quidditch</div>
        </li>
        <li>
            <i class="fas fa-quote-left fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-quote-left</div>
        </li>
        <li>
            <i class="fas fa-quote-right fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-quote-right</div>
        </li>
        <li>
            <i class="fas fa-quran fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-quran</div>
        </li>
        <li>
            <i class="fas fa-radiation fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-radiation</div>
        </li>
        <li>
            <i class="fas fa-radiation-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-radiation-alt</div>
        </li>
        <li>
            <i class="fas fa-rainbow fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-rainbow</div>
        </li>
        <li>
            <i class="fas fa-random fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-random</div>
        </li>
        <li>
            <i class="fas fa-receipt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-receipt</div>
        </li>
        <li>
            <i class="fas fa-record-vinyl fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-record-vinyl</div>
        </li>
        <li>
            <i class="fas fa-recycle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-recycle</div>
        </li>
        <li>
            <i class="fas fa-redo fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-redo</div>
        </li>
        <li>
            <i class="fas fa-redo-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-redo-alt</div>
        </li>
        <li>
            <i class="fas fa-registered fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-registered</div>
        </li>
        <li>
            <i class="fas fa-remove-format fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-remove-format</div>
        </li>
        <li>
            <i class="fas fa-reply fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-reply</div>
        </li>
        <li>
            <i class="fas fa-reply-all fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-reply-all</div>
        </li>
        <li>
            <i class="fas fa-republican fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-republican</div>
        </li>
        <li>
            <i class="fas fa-restroom fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-restroom</div>
        </li>
        <li>
            <i class="fas fa-retweet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-retweet</div>
        </li>
        <li>
            <i class="fas fa-ribbon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ribbon</div>
        </li>
        <li>
            <i class="fas fa-ring fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ring</div>
        </li>
        <li>
            <i class="fas fa-road fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-road</div>
        </li>
        <li>
            <i class="fas fa-robot fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-robot</div>
        </li>
        <li>
            <i class="fas fa-rocket fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-rocket</div>
        </li>
        <li>
            <i class="fas fa-route fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-route</div>
        </li>
        <li>
            <i class="fas fa-rss fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-rss</div>
        </li>
        <li>
            <i class="fas fa-rss-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-rss-square</div>
        </li>
        <li>
            <i class="fas fa-ruble-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ruble-sign</div>
        </li>
        <li>
            <i class="fas fa-ruler fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ruler</div>
        </li>
        <li>
            <i class="fas fa-ruler-combined fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ruler-combined</div>
        </li>
        <li>
            <i class="fas fa-ruler-horizontal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ruler-horizontal</div>
        </li>
        <li>
            <i class="fas fa-ruler-vertical fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ruler-vertical</div>
        </li>
        <li>
            <i class="fas fa-running fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-running</div>
        </li>
        <li>
            <i class="fas fa-rupee-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-rupee-sign</div>
        </li>
        <li>
            <i class="fas fa-sad-cry fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sad-cry</div>
        </li>
        <li>
            <i class="fas fa-sad-tear fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sad-tear</div>
        </li>
        <li>
            <i class="fas fa-satellite fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-satellite</div>
        </li>
        <li>
            <i class="fas fa-satellite-dish fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-satellite-dish</div>
        </li>
        <li>
            <i class="fas fa-save fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-save</div>
        </li>
        <li>
            <i class="fas fa-school fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-school</div>
        </li>
        <li>
            <i class="fas fa-screwdriver fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-screwdriver</div>
        </li>
        <li>
            <i class="fas fa-scroll fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-scroll</div>
        </li>
        <li>
            <i class="fas fa-sd-card fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sd-card</div>
        </li>
        <li>
            <i class="fas fa-search fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-search</div>
        </li>
        <li>
            <i class="fas fa-search-dollar fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-search-dollar</div>
        </li>
        <li>
            <i class="fas fa-search-location fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-search-location</div>
        </li>
        <li>
            <i class="fas fa-search-minus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-search-minus</div>
        </li>
        <li>
            <i class="fas fa-search-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-search-plus</div>
        </li>
        <li>
            <i class="fas fa-seedling fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-seedling</div>
        </li>
        <li>
            <i class="fas fa-server fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-server</div>
        </li>
        <li>
            <i class="fas fa-shapes fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shapes</div>
        </li>
        <li>
            <i class="fas fa-share fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-share</div>
        </li>
        <li>
            <i class="fas fa-share-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-share-alt</div>
        </li>
        <li>
            <i class="fas fa-share-alt-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-share-alt-square</div>
        </li>
        <li>
            <i class="fas fa-share-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-share-square</div>
        </li>
        <li>
            <i class="fas fa-shekel-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shekel-sign</div>
        </li>
        <li>
            <i class="fas fa-shield-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shield-alt</div>
        </li>
        <li>
            <i class="fas fa-ship fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ship</div>
        </li>
        <li>
            <i class="fas fa-shipping-fast fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shipping-fast</div>
        </li>
        <li>
            <i class="fas fa-shoe-prints fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shoe-prints</div>
        </li>
        <li>
            <i class="fas fa-shopping-bag fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shopping-bag</div>
        </li>
        <li>
            <i class="fas fa-shopping-basket fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shopping-basket</div>
        </li>
        <li>
            <i class="fas fa-shopping-cart fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shopping-cart</div>
        </li>
        <li>
            <i class="fas fa-shower fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shower</div>
        </li>
        <li>
            <i class="fas fa-shuttle-van fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-shuttle-van</div>
        </li>
        <li>
            <i class="fas fa-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sign</div>
        </li>
        <li>
            <i class="fas fa-sign-in-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sign-in-alt</div>
        </li>
        <li>
            <i class="fas fa-sign-language fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sign-language</div>
        </li>
        <li>
            <i class="fas fa-sign-out-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sign-out-alt</div>
        </li>
        <li>
            <i class="fas fa-signal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-signal</div>
        </li>
        <li>
            <i class="fas fa-signature fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-signature</div>
        </li>
        <li>
            <i class="fas fa-sim-card fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sim-card</div>
        </li>
        <li>
            <i class="fas fa-sitemap fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sitemap</div>
        </li>
        <li>
            <i class="fas fa-skating fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-skating</div>
        </li>
        <li>
            <i class="fas fa-skiing fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-skiing</div>
        </li>
        <li>
            <i class="fas fa-skiing-nordic fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-skiing-nordic</div>
        </li>
        <li>
            <i class="fas fa-skull fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-skull</div>
        </li>
        <li>
            <i class="fas fa-skull-crossbones fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-skull-crossbones</div>
        </li>
        <li>
            <i class="fas fa-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-slash</div>
        </li>
        <li>
            <i class="fas fa-sleigh fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sleigh</div>
        </li>
        <li>
            <i class="fas fa-sliders-h fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sliders-h</div>
        </li>
        <li>
            <i class="fas fa-smile fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smile</div>
        </li>
        <li>
            <i class="fas fa-smile-beam fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smile-beam</div>
        </li>
        <li>
            <i class="fas fa-smile-wink fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smile-wink</div>
        </li>
        <li>
            <i class="fas fa-smog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smog</div>
        </li>
        <li>
            <i class="fas fa-smoking fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smoking</div>
        </li>
        <li>
            <i class="fas fa-smoking-ban fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-smoking-ban</div>
        </li>
        <li>
            <i class="fas fa-sms fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sms</div>
        </li>
        <li>
            <i class="fas fa-snowboarding fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-snowboarding</div>
        </li>
        <li>
            <i class="fas fa-snowflake fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-snowflake</div>
        </li>
        <li>
            <i class="fas fa-snowman fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-snowman</div>
        </li>
        <li>
            <i class="fas fa-snowplow fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-snowplow</div>
        </li>
        <li>
            <i class="fas fa-socks fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-socks</div>
        </li>
        <li>
            <i class="fas fa-solar-panel fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-solar-panel</div>
        </li>
        <li>
            <i class="fas fa-sort fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort</div>
        </li>
        <li>
            <i class="fas fa-sort-alpha-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-alpha-down</div>
        </li>
        <li>
            <i class="fas fa-sort-alpha-down-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-alpha-down-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-alpha-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-alpha-up</div>
        </li>
        <li>
            <i class="fas fa-sort-alpha-up-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-alpha-up-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-amount-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-amount-down</div>
        </li>
        <li>
            <i class="fas fa-sort-amount-down-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-amount-down-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-amount-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-amount-up</div>
        </li>
        <li>
            <i class="fas fa-sort-amount-up-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-amount-up-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-down</div>
        </li>
        <li>
            <i class="fas fa-sort-numeric-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-numeric-down</div>
        </li>
        <li>
            <i class="fas fa-sort-numeric-down-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-numeric-down-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-numeric-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-numeric-up</div>
        </li>
        <li>
            <i class="fas fa-sort-numeric-up-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-numeric-up-alt</div>
        </li>
        <li>
            <i class="fas fa-sort-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sort-up</div>
        </li>
        <li>
            <i class="fas fa-spa fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-spa</div>
        </li>
        <li>
            <i class="fas fa-space-shuttle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-space-shuttle</div>
        </li>
        <li>
            <i class="fas fa-spell-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-spell-check</div>
        </li>
        <li>
            <i class="fas fa-spider fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-spider</div>
        </li>
        <li>
            <i class="fas fa-spinner fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-spinner</div>
        </li>
        <li>
            <i class="fas fa-splotch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-splotch</div>
        </li>
        <li>
            <i class="fas fa-spray-can fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-spray-can</div>
        </li>
        <li>
            <i class="fas fa-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-square</div>
        </li>
        <li>
            <i class="fas fa-square-full fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-square-full</div>
        </li>
        <li>
            <i class="fas fa-square-root-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-square-root-alt</div>
        </li>
        <li>
            <i class="fas fa-stamp fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stamp</div>
        </li>
        <li>
            <i class="fas fa-star fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star</div>
        </li>
        <li>
            <i class="fas fa-star-and-crescent fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star-and-crescent</div>
        </li>
        <li>
            <i class="fas fa-star-half fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star-half</div>
        </li>
        <li>
            <i class="fas fa-star-half-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star-half-alt</div>
        </li>
        <li>
            <i class="fas fa-star-of-david fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star-of-david</div>
        </li>
        <li>
            <i class="fas fa-star-of-life fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-star-of-life</div>
        </li>
        <li>
            <i class="fas fa-step-backward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-step-backward</div>
        </li>
        <li>
            <i class="fas fa-step-forward fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-step-forward</div>
        </li>
        <li>
            <i class="fas fa-stethoscope fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stethoscope</div>
        </li>
        <li>
            <i class="fas fa-sticky-note fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sticky-note</div>
        </li>
        <li>
            <i class="fas fa-stop fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stop</div>
        </li>
        <li>
            <i class="fas fa-stop-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stop-circle</div>
        </li>
        <li>
            <i class="fas fa-stopwatch fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stopwatch</div>
        </li>
        <li>
            <i class="fas fa-store fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-store</div>
        </li>
        <li>
            <i class="fas fa-store-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-store-alt</div>
        </li>
        <li>
            <i class="fas fa-stream fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stream</div>
        </li>
        <li>
            <i class="fas fa-street-view fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-street-view</div>
        </li>
        <li>
            <i class="fas fa-strikethrough fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-strikethrough</div>
        </li>
        <li>
            <i class="fas fa-stroopwafel fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-stroopwafel</div>
        </li>
        <li>
            <i class="fas fa-subscript fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-subscript</div>
        </li>
        <li>
            <i class="fas fa-subway fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-subway</div>
        </li>
        <li>
            <i class="fas fa-suitcase fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-suitcase</div>
        </li>
        <li>
            <i class="fas fa-suitcase-rolling fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-suitcase-rolling</div>
        </li>
        <li>
            <i class="fas fa-sun fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sun</div>
        </li>
        <li>
            <i class="fas fa-superscript fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-superscript</div>
        </li>
        <li>
            <i class="fas fa-surprise fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-surprise</div>
        </li>
        <li>
            <i class="fas fa-swatchbook fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-swatchbook</div>
        </li>
        <li>
            <i class="fas fa-swimmer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-swimmer</div>
        </li>
        <li>
            <i class="fas fa-swimming-pool fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-swimming-pool</div>
        </li>
        <li>
            <i class="fas fa-synagogue fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-synagogue</div>
        </li>
        <li>
            <i class="fas fa-sync fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sync</div>
        </li>
        <li>
            <i class="fas fa-sync-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-sync-alt</div>
        </li>
        <li>
            <i class="fas fa-syringe fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-syringe</div>
        </li>
        <li>
            <i class="fas fa-table fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-table</div>
        </li>
        <li>
            <i class="fas fa-table-tennis fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-table-tennis</div>
        </li>
        <li>
            <i class="fas fa-tablet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tablet</div>
        </li>
        <li>
            <i class="fas fa-tablet-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tablet-alt</div>
        </li>
        <li>
            <i class="fas fa-tablets fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tablets</div>
        </li>
        <li>
            <i class="fas fa-tachometer-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tachometer-alt</div>
        </li>
        <li>
            <i class="fas fa-tag fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tag</div>
        </li>
        <li>
            <i class="fas fa-tags fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tags</div>
        </li>
        <li>
            <i class="fas fa-tape fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tape</div>
        </li>
        <li>
            <i class="fas fa-tasks fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tasks</div>
        </li>
        <li>
            <i class="fas fa-taxi fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-taxi</div>
        </li>
        <li>
            <i class="fas fa-teeth fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-teeth</div>
        </li>
        <li>
            <i class="fas fa-teeth-open fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-teeth-open</div>
        </li>
        <li>
            <i class="fas fa-temperature-high fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-temperature-high</div>
        </li>
        <li>
            <i class="fas fa-temperature-low fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-temperature-low</div>
        </li>
        <li>
            <i class="fas fa-tenge fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tenge</div>
        </li>
        <li>
            <i class="fas fa-terminal fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-terminal</div>
        </li>
        <li>
            <i class="fas fa-text-height fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-text-height</div>
        </li>
        <li>
            <i class="fas fa-text-width fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-text-width</div>
        </li>
        <li>
            <i class="fas fa-th fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-th</div>
        </li>
        <li>
            <i class="fas fa-th-large fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-th-large</div>
        </li>
        <li>
            <i class="fas fa-th-list fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-th-list</div>
        </li>
        <li>
            <i class="fas fa-theater-masks fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-theater-masks</div>
        </li>
        <li>
            <i class="fas fa-thermometer fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer</div>
        </li>
        <li>
            <i class="fas fa-thermometer-empty fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer-empty</div>
        </li>
        <li>
            <i class="fas fa-thermometer-full fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer-full</div>
        </li>
        <li>
            <i class="fas fa-thermometer-half fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer-half</div>
        </li>
        <li>
            <i class="fas fa-thermometer-quarter fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer-quarter</div>
        </li>
        <li>
            <i class="fas fa-thermometer-three-quarters fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thermometer-three-quarters</div>
        </li>
        <li>
            <i class="fas fa-thumbs-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thumbs-down</div>
        </li>
        <li>
            <i class="fas fa-thumbs-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thumbs-up</div>
        </li>
        <li>
            <i class="fas fa-thumbtack fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-thumbtack</div>
        </li>
        <li>
            <i class="fas fa-ticket-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-ticket-alt</div>
        </li>
        <li>
            <i class="fas fa-times fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-times</div>
        </li>
        <li>
            <i class="fas fa-times-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-times-circle</div>
        </li>
        <li>
            <i class="fas fa-tint fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tint</div>
        </li>
        <li>
            <i class="fas fa-tint-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tint-slash</div>
        </li>
        <li>
            <i class="fas fa-tired fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tired</div>
        </li>
        <li>
            <i class="fas fa-toggle-off fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-toggle-off</div>
        </li>
        <li>
            <i class="fas fa-toggle-on fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-toggle-on</div>
        </li>
        <li>
            <i class="fas fa-toilet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-toilet</div>
        </li>
        <li>
            <i class="fas fa-toilet-paper fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-toilet-paper</div>
        </li>
        <li>
            <i class="fas fa-toolbox fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-toolbox</div>
        </li>
        <li>
            <i class="fas fa-tools fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tools</div>
        </li>
        <li>
            <i class="fas fa-tooth fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tooth</div>
        </li>
        <li>
            <i class="fas fa-torah fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-torah</div>
        </li>
        <li>
            <i class="fas fa-torii-gate fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-torii-gate</div>
        </li>
        <li>
            <i class="fas fa-tractor fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tractor</div>
        </li>
        <li>
            <i class="fas fa-trademark fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trademark</div>
        </li>
        <li>
            <i class="fas fa-traffic-light fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-traffic-light</div>
        </li>
        <li>
            <i class="fas fa-train fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-train</div>
        </li>
        <li>
            <i class="fas fa-tram fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tram</div>
        </li>
        <li>
            <i class="fas fa-transgender fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-transgender</div>
        </li>
        <li>
            <i class="fas fa-transgender-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-transgender-alt</div>
        </li>
        <li>
            <i class="fas fa-trash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trash</div>
        </li>
        <li>
            <i class="fas fa-trash-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trash-alt</div>
        </li>
        <li>
            <i class="fas fa-trash-restore fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trash-restore</div>
        </li>
        <li>
            <i class="fas fa-trash-restore-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trash-restore-alt</div>
        </li>
        <li>
            <i class="fas fa-tree fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tree</div>
        </li>
        <li>
            <i class="fas fa-trophy fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-trophy</div>
        </li>
        <li>
            <i class="fas fa-truck fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-truck</div>
        </li>
        <li>
            <i class="fas fa-truck-loading fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-truck-loading</div>
        </li>
        <li>
            <i class="fas fa-truck-monster fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-truck-monster</div>
        </li>
        <li>
            <i class="fas fa-truck-moving fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-truck-moving</div>
        </li>
        <li>
            <i class="fas fa-truck-pickup fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-truck-pickup</div>
        </li>
        <li>
            <i class="fas fa-tshirt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tshirt</div>
        </li>
        <li>
            <i class="fas fa-tty fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tty</div>
        </li>
        <li>
            <i class="fas fa-tv fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-tv</div>
        </li>
        <li>
            <i class="fas fa-umbrella fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-umbrella</div>
        </li>
        <li>
            <i class="fas fa-umbrella-beach fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-umbrella-beach</div>
        </li>
        <li>
            <i class="fas fa-underline fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-underline</div>
        </li>
        <li>
            <i class="fas fa-undo fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-undo</div>
        </li>
        <li>
            <i class="fas fa-undo-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-undo-alt</div>
        </li>
        <li>
            <i class="fas fa-universal-access fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-universal-access</div>
        </li>
        <li>
            <i class="fas fa-university fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-university</div>
        </li>
        <li>
            <i class="fas fa-unlink fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-unlink</div>
        </li>
        <li>
            <i class="fas fa-unlock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-unlock</div>
        </li>
        <li>
            <i class="fas fa-unlock-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-unlock-alt</div>
        </li>
        <li>
            <i class="fas fa-upload fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-upload</div>
        </li>
        <li>
            <i class="fas fa-user fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user</div>
        </li>
        <li>
            <i class="fas fa-user-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-alt</div>
        </li>
        <li>
            <i class="fas fa-user-alt-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-alt-slash</div>
        </li>
        <li>
            <i class="fas fa-user-astronaut fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-astronaut</div>
        </li>
        <li>
            <i class="fas fa-user-check fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-check</div>
        </li>
        <li>
            <i class="fas fa-user-circle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-circle</div>
        </li>
        <li>
            <i class="fas fa-user-clock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-clock</div>
        </li>
        <li>
            <i class="fas fa-user-cog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-cog</div>
        </li>
        <li>
            <i class="fas fa-user-edit fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-edit</div>
        </li>
        <li>
            <i class="fas fa-user-friends fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-friends</div>
        </li>
        <li>
            <i class="fas fa-user-graduate fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-graduate</div>
        </li>
        <li>
            <i class="fas fa-user-injured fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-injured</div>
        </li>
        <li>
            <i class="fas fa-user-lock fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-lock</div>
        </li>
        <li>
            <i class="fas fa-user-md fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-md</div>
        </li>
        <li>
            <i class="fas fa-user-minus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-minus</div>
        </li>
        <li>
            <i class="fas fa-user-ninja fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-ninja</div>
        </li>
        <li>
            <i class="fas fa-user-nurse fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-nurse</div>
        </li>
        <li>
            <i class="fas fa-user-plus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-plus</div>
        </li>
        <li>
            <i class="fas fa-user-secret fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-secret</div>
        </li>
        <li>
            <i class="fas fa-user-shield fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-shield</div>
        </li>
        <li>
            <i class="fas fa-user-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-slash</div>
        </li>
        <li>
            <i class="fas fa-user-tag fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-tag</div>
        </li>
        <li>
            <i class="fas fa-user-tie fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-tie</div>
        </li>
        <li>
            <i class="fas fa-user-times fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-user-times</div>
        </li>
        <li>
            <i class="fas fa-users fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-users</div>
        </li>
        <li>
            <i class="fas fa-users-cog fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-users-cog</div>
        </li>
        <li>
            <i class="fas fa-utensil-spoon fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-utensil-spoon</div>
        </li>
        <li>
            <i class="fas fa-utensils fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-utensils</div>
        </li>
        <li>
            <i class="fas fa-vector-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vector-square</div>
        </li>
        <li>
            <i class="fas fa-venus fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-venus</div>
        </li>
        <li>
            <i class="fas fa-venus-double fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-venus-double</div>
        </li>
        <li>
            <i class="fas fa-venus-mars fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-venus-mars</div>
        </li>
        <li>
            <i class="fas fa-vial fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vial</div>
        </li>
        <li>
            <i class="fas fa-vials fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vials</div>
        </li>
        <li>
            <i class="fas fa-video fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-video</div>
        </li>
        <li>
            <i class="fas fa-video-slash fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-video-slash</div>
        </li>
        <li>
            <i class="fas fa-vihara fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vihara</div>
        </li>
        <li>
            <i class="fas fa-voicemail fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-voicemail</div>
        </li>
        <li>
            <i class="fas fa-volleyball-ball fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-volleyball-ball</div>
        </li>
        <li>
            <i class="fas fa-volume-down fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-volume-down</div>
        </li>
        <li>
            <i class="fas fa-volume-mute fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-volume-mute</div>
        </li>
        <li>
            <i class="fas fa-volume-off fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-volume-off</div>
        </li>
        <li>
            <i class="fas fa-volume-up fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-volume-up</div>
        </li>
        <li>
            <i class="fas fa-vote-yea fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vote-yea</div>
        </li>
        <li>
            <i class="fas fa-vr-cardboard fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-vr-cardboard</div>
        </li>
        <li>
            <i class="fas fa-walking fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-walking</div>
        </li>
        <li>
            <i class="fas fa-wallet fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wallet</div>
        </li>
        <li>
            <i class="fas fa-warehouse fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-warehouse</div>
        </li>
        <li>
            <i class="fas fa-water fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-water</div>
        </li>
        <li>
            <i class="fas fa-wave-square fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wave-square</div>
        </li>
        <li>
            <i class="fas fa-weight fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-weight</div>
        </li>
        <li>
            <i class="fas fa-weight-hanging fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-weight-hanging</div>
        </li>
        <li>
            <i class="fas fa-wheelchair fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wheelchair</div>
        </li>
        <li>
            <i class="fas fa-wifi fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wifi</div>
        </li>
        <li>
            <i class="fas fa-wind fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wind</div>
        </li>
        <li>
            <i class="fas fa-window-close fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-window-close</div>
        </li>
        <li>
            <i class="fas fa-window-maximize fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-window-maximize</div>
        </li>
        <li>
            <i class="fas fa-window-minimize fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-window-minimize</div>
        </li>
        <li>
            <i class="fas fa-window-restore fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-window-restore</div>
        </li>
        <li>
            <i class="fas fa-wine-bottle fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wine-bottle</div>
        </li>
        <li>
            <i class="fas fa-wine-glass fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wine-glass</div>
        </li>
        <li>
            <i class="fas fa-wine-glass-alt fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wine-glass-alt</div>
        </li>
        <li>
            <i class="fas fa-won-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-won-sign</div>
        </li>
        <li>
            <i class="fas fa-wrench fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-wrench</div>
        </li>
        <li>
            <i class="fas fa-x-ray fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-x-ray</div>
        </li>
        <li>
            <i class="fas fa-yen-sign fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-yen-sign</div>
        </li>
        <li>
            <i class="fas fa-yin-yang fa-2x"></i>
            <!-- uses solid style -->
            <div>fas fa-yin-yang</div>
        </li>



    </ul>



</body>
</html>
