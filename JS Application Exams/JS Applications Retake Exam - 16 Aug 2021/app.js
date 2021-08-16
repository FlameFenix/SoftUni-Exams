import page from "./node_modules/page/page.mjs";
import authService from "./services/authService.js";
import gamesService from "./services/gamesService.js";
import { LitRenderer } from "./rendering/litRenderer.js";
import navigation from "./pages/navigation/navigation.js";
import loginPage from "./pages/login/login.js";
import registerPage from "./pages/register/register.js";
import cataloguePage from "./pages/catalogue/catalogue.js";
import homePage from "./pages/home/home.js";
import createPage from "./pages/create/create.js";
import detailsPage from "./pages/details/details.js";

let navElement = document.querySelector('header');
let mainElement = document.querySelector('#main-content');

let renderer = new LitRenderer();

let navRenderHandler = renderer.createRenderHandler(navElement);
let mainRenderHandler = renderer.createRenderHandler(mainElement);

navigation.initialize(page,navRenderHandler, authService);
loginPage.initialize(page, mainRenderHandler, authService);
registerPage.initialize(page, mainRenderHandler, authService);
cataloguePage.initialize(page, mainRenderHandler, gamesService);
homePage.initialize(page, mainRenderHandler, gamesService);
createPage.initialize(page, mainRenderHandler, gamesService);
detailsPage.initialize(page, mainRenderHandler, gamesService);

page('/', '/home');
page('/index.html', '/home');

page(decorateContextWithUser);
page(navigation.getView);

page('/login', loginPage.getView);
page('/register', registerPage.getView);
page('/catalogue', cataloguePage.getView);
page('/home', homePage.getView);
page('/create', createPage.getView);
page('/details/:id', detailsPage.getView);

page.start();

function decorateContextWithUser(context, next){
    let user = authService.getUser();
    context.user = user;
    next();
}
