import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Book List",
    component: () => import("../views/Book/BookList.vue")
  },
  {
    path: "/book-form",
    name: "Book Formulary",
    component: () => import("../views/Book/BookFormulary.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
