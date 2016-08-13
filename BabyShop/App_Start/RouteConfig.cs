using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BabyShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
    new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
name: "Taikhoan",
url: "tai-khoan",
defaults: new { controller = "User", action = "Info", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
name: "Quenpass",
url: "quen-mat-khau",
defaults: new { controller = "User", action = "Forgotpassword", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);


            routes.MapRoute(
name: "capnhatthongtin",
url: "cap-nhat-thong-tin",
defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);


            routes.MapRoute(
name: "doimatkhau",
url: "doi-mat-khau",
defaults: new { controller = "User", action = "Changepass", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
  name: "About",
  url: "gioi-thieu",
  defaults: new { controller = "About", action = "About", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
name: "Chinh sach bao mat",
url: "chinh-sach-bao-mat",
defaults: new { controller = "About", action = "Chinhsachbaomat", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
  name: "Dieukhoansudung",
  url: "dieu-khoan-su-dung",
  defaults: new { controller = "About", action = "Dieukhoansudung", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
name: "Huongdanmuahang",
url: "huong-dan-mua-hang",
defaults: new { controller = "About", action = "Huongdanmuahang", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
  name: "Hinhthucthanhtoan",
  url: "hinh-thuc-thanh-toan",
  defaults: new { controller = "About", action = "Hinhthucthanhtoan", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
name: "Chinhsachgiaohang",
url: "chinh-sach-giao-hang",
defaults: new { controller = "About", action = "Chinhsachgiaohang", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);


            routes.MapRoute(
  name: "Chinhsachbaohanh",
  url: "chinh-sach-bao-hanh",
  defaults: new { controller = "About", action = "Chinhsachbaohanh", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
name: "Chinhsachdoitrahang",
url: "chinh-sach-doi-tra-hang",
defaults: new { controller = "About", action = "Chinhsachdoitrahang", id = UrlParameter.Optional },
namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
     name: "Contact",
     url: "lien-he",
     defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
     namespaces: new[] { "BabyShop.Controllers" }
 );
            routes.MapRoute(
           name: "Tags",
           url: "bai-viet/tag/{tagId}",
           defaults: new { controller = "Post", action = "Tag", id = UrlParameter.Optional },
           namespaces: new[] { "BabyShop.Controllers" }
       );

            routes.MapRoute(
  name: "Post",
  url: "bai-viet/{metatitle}-{cateId}",
  defaults: new { controller = "Post", action = "Category", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);
            routes.MapRoute(
             name: "Content Detail",
             url: "chi-tiet-bai-viet/{metatitle}-{id}",
             defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
             namespaces: new[] { "BabyShop.Controllers" }
         );

            routes.MapRoute(
           name: "Tags Product",
           url: "san-pham/tag/{tagId}",
           defaults: new { controller = "Product", action = "Tag", id = UrlParameter.Optional },
           namespaces: new[] { "BabyShop.Controllers" }
       );

            routes.MapRoute(
         name: "Login",
         url: "dang-nhap",
         defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
         namespaces: new[] { "BabyShop.Controllers" }
     );

            routes.MapRoute(
               name: "Product Category",
                url: "san-pham/{metatitle}-{cateId}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "BabyShop.Controllers" }
           );

            routes.MapRoute(
       name: "ViewAllProduct",
       url: "san-pham",
       defaults: new { controller = "Product", action = "ViewAllProduct", id = UrlParameter.Optional },
       namespaces: new[] { "BabyShop.Controllers" }
   );
            routes.MapRoute(
    name: "ViewAllSaleOffProduct",
    url: "san-pham-khuyen-mai",
    defaults: new { controller = "Product", action = "ViewAllSaleOffProduct", id = UrlParameter.Optional },
    namespaces: new[] { "BabyShop.Controllers" }
    );

            routes.MapRoute(
  name: "ViewAllHotProduct",
  url: "san-pham-ban-chay",
  defaults: new { controller = "Product", action = "ViewAllHotProduct", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
  );

            routes.MapRoute(
          name: "Search",
          url: "tim-kiem",
          defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
          namespaces: new[] { "BabyShop.Controllers" }
      );


            routes.MapRoute(
            name: "Product Detail",
            url: "chi-tiet/{metatitle}-{id}",
            defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
            namespaces: new[] { "BabyShop.Controllers" }
        );

            routes.MapRoute(
      name: "Register",
      url: "dang-ky",
      defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
      namespaces: new[] { "BabyShop.Controllers" }
  );

            routes.MapRoute(
     name: "Cart",
     url: "gio-hang",
     defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
     namespaces: new[] { "BabyShop.Controllers" }
 );
            routes.MapRoute(
       name: "Payment",
       url: "thanh-toan",
       defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
       namespaces: new[] { "BabyShop.Controllers" }
      );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "BabyShop.Controllers" }
            );
        }
    }
}
