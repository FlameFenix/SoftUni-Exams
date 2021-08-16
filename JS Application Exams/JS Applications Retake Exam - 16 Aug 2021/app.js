import page from "./node_modules/page/page.mjs";
import authService from "./services/authService.js";
import gamesService from "./services/gamesService.js";

page('/', '/home');
page('/index.html', '/home');

page.start();