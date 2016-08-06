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
  name: "About",
  url: "gioi-thieu",
  defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
  namespaces: new[] { "BabyShop.Controllers" }
);

            routes.MapRoute(
             name: "Content Detail",
             url: "cam-nang-cho-be/{metatitle}-{id}",
             defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
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
           url: "tag/{tagId}",
           defaults: new { controller = "Post", action = "Tag", id = UrlParameter.Optional },
           namespaces: new[] { "BabyShop.Controllers" }
       );


            routes.MapRoute(
           name: "Tags Product",
           url: "san-pham/tag/{tagId}",
           defaults: new { controller = "Product", action = "Tag", id = UrlParameter.Optional },
           namespaces: new[] { "BabyShop.Controllers" }
       );

            routes.MapRoute(
      name: "Post",
      url: "cam-nang-cho-be",
      defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
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
