import page from "./node_modules/page/page.mjs";

import authService from "./services/authService.js";
import memesService from "./services/memesService.js";
import { LitRenderer } from "./rendering/litRenderer.js";
import navigation from "./pages/navigation/navigation.js";
import loginPage from "./pages/login/login.js";
import registerPage from "./pages/register/register.js";
import createPage from "./pages/create/create.js";
import allMemesPage from "./pages/allmemes/allMemes.js";
import homePage from "./pages/home/home.js";
import detailsPage from "./pages/details/details.js";
import profilePage from "./pages/profile/profile.js";
import editPage from "./pages/edit/edit.js";

let navElement = document.querySelector('nav');
let mainElement = document.querySelector('main');

let renderer = new LitRenderer();

let navRenderHandler = renderer.createRenderHandler(navElement);
let mainRenderHandler = renderer.createRenderHandler(mainElement);


navigation.initialize(page, navRenderHandler, authService);
loginPage.initialize(page, mainRenderHandler, authService);
homePage.initialize(page, mainRenderHandler, authService);
registerPage.initialize(page, mainRenderHandler, authService);
createPage.initialize(page, mainRenderHandler, memesService);
allMemesPage.initialize(page, mainRenderHandler, memesService);
detailsPage.initialize(page, mainRenderHandler, memesService);
profilePage.initialize(page, mainRenderHandler, memesService);
editPage.initialize(page, mainRenderHandler, memesService);

page('/', '/home');
page('/index.html', '/home');

page(decorateContextWithUser);
page(navigation.getView);

page('/home', homePage.getView);
page('/all-memes', allMemesPage.getView);
page('/login', loginPage.getView);
page('/register', registerPage.getView);
page('/create', createPage.getView);
page('/details/:id', detailsPage.getView);
page('/edit/:memeId', editPage.getView);
page('/profile', profilePage.getView);



page.start();

function decorateContextWithUser(context, next){
    let user = authService.getUser();
    context.user = user;
    next();
}
