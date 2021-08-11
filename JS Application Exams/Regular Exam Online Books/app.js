import page from "./node_modules/page/page.mjs";
import { LitRenderer } from "./rendering/litRenderer.js";

import authService from "./services/authService.js";
import booksService from "./services/booksService.js";

import navigation from "./pages/navigation/navigation.js";
import loginPage from "./pages/login/login.js";
import registerPage from "./pages/register/register.js";
import createPage from "./pages/create/create.js";
import dashboardPage from "./pages/dashboard/dashboard.js";
import detailsPage from "./pages/details/details.js";
import editPage from "./pages/edit/edit.js";
import mybooksPage from "./pages/mybooks/mybooks.js";

let navElement = document.querySelector('.navbar');
let mainElement = document.getElementById('site-content');

let renderer = new LitRenderer();

let navRenderHandler = renderer.createRenderHandler(navElement);
let mainRenderHandler = renderer.createRenderHandler(mainElement);

navigation.initialize(page, navRenderHandler, authService);
loginPage.initialize(page, mainRenderHandler, authService);
registerPage.initialize(page, mainRenderHandler, authService);
createPage.initialize(page, mainRenderHandler, booksService);
dashboardPage.initialize(page, mainRenderHandler, booksService);
detailsPage.initialize(page, mainRenderHandler, booksService);
editPage.initialize(page, mainRenderHandler, booksService);
mybooksPage.initialize(page, mainRenderHandler, booksService);

page('/index.html', '/home');
page('/', '/home');

page(decorateContextWithUser);
page(navigation.getView);

page('/login', loginPage.getView);
page('/register', registerPage.getView);
page('/create', createPage.getView);
page('/dashboard', dashboardPage.getView);
page('/details/:id', detailsPage.getView);
page('/edit/:bookId', editPage.getView);
page('/mybooks', mybooksPage.getView);

page.start();


function decorateContextWithUser(context, next){
    let user = authService.getUser();
    context.user = user;
    next();
}
