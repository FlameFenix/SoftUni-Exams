
import page from "./node_modules/page/page.mjs";
import { LitRenderer } from "./rendering/litRenderer.js";


import authService from "./services/authService.js";
import listingsService from "./services/listingsService.js";

import navigation from "./pages/navigation/nav.js";
import registerPage from "./pages/register/register.js";
import loginPage from "./pages/login/login.js";
import homePage from "./pages/home/home.js";
import listingsPage from "./pages/listings/listings.js";
import createPage from "./pages/create/create.js";
import detailsPage from "./pages/details/details.js";
import editPage from "./pages/edit/edit.js";
import myListingsPage from "./pages/mylistings/myListings.js";

let navigationElement = document.getElementById('navigation');
let mainElement = document.getElementById('site-content');

let renderer = new LitRenderer();

let navRenderHandler = renderer.createRenderHandler(navigationElement);
let mainRenderHandler = renderer.createRenderHandler(mainElement);

navigation.initialize(page, navRenderHandler, authService);
registerPage.initialize(page, mainRenderHandler, authService);
loginPage.initialize(page, mainRenderHandler, authService);
homePage.initialize(page, mainRenderHandler, listingsService)
listingsPage.initialize(page, mainRenderHandler, listingsService);
createPage.initialize(page, mainRenderHandler, listingsService);
detailsPage.initialize(page, mainRenderHandler, listingsService);
editPage.initialize(page, mainRenderHandler, listingsService);
myListingsPage.initialize(page, mainRenderHandler, listingsService);

page('index.html', '/home');
page('/', '/home');

page(decorateContextWithUser);
page(navigation.getView);

page('/register', registerPage.getView);
page('/login', loginPage.getView);
page('/home', homePage.getView);
page('/listings', listingsPage.getView);
page('/create', createPage.getView);
page('/details/:id', detailsPage.getView);
page('/edit/:listingId', editPage.getView);
page('/mylistings', myListingsPage.getView);

page.start();

function decorateContextWithUser(context, next) {
    let user = authService.getUser();
    context.user = user;
    next();
}