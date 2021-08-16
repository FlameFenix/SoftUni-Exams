import { editTemplate } from "./editTemplate.js";

let _router = undefined;
let _renderHandler = undefined;
let _gamesService = undefined;
let _form = undefined;

function initialize(router, renderHandler, gamesService) {
    _router = router;
    _renderHandler = renderHandler;
    _gamesService = gamesService;
}

async function submitHandler(id, e){
    e.preventDefault();
    try{
        let formData = new FormData(e.target);

        let emptyField = false;
        let title = formData.get('title');
        if(title.trim() === ''){
            window.alert('Title is required');
            emptyField = true;
        }

        let category = formData.get('category');
        if(category.trim() === ''){
            window.alert('Category is required');
            emptyField = true;
        }

        let maxLevel = formData.get('maxLevel');
        if(maxLevel.trim() === ''){
            window.alert('MaxLevel is required');
            emptyField = true;
        }

        let imageUrl = formData.get('imageUrl');
        if(imageUrl.trim() === ''){
            window.alert('Image Url is required');
            emptyField = true;
        }

        let summary = formData.get('summary');
        if(summary.trim() === ''){
            window.alert('Summary is required');
            emptyField = true;
        }

        if(emptyField) {
            return;
        }

        let game = {
            title,
            category,
            maxLevel,
            imageUrl,
            summary
        }
    
        await _gamesService.update(game, id);
        _router.redirect(`/details/${id}`);
    } catch (err){
        alert(err);
    }
   
}

async function getView(context) {
    let id = context.params.gameId;
    let game = await _gamesService.get(id);

    _form = {
        submitHandler,
        game
    }
    let templateResult = editTemplate(_form);
    _renderHandler(templateResult);
}

export default {
    getView,
    initialize
}