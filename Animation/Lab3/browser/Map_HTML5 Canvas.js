(function (cjs, an) {

var p; // shortcut to reference prototypes
var lib={};var ss={};var img={};
lib.ssMetadata = [
		{name:"Map_HTML5 Canvas_atlas_1", frames: [[0,0,1037,683]]}
];


(lib.AnMovieClip = function(){
	this.actionFrames = [];
	this.ignorePause = false;
	this.gotoAndPlay = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.play = function(){
		cjs.MovieClip.prototype.play.call(this);
	}
	this.gotoAndStop = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndStop.call(this,positionOrLabel);
	}
	this.stop = function(){
		cjs.MovieClip.prototype.stop.call(this);
	}
}).prototype = p = new cjs.MovieClip();
// symbols:



(lib.карта_Минска = function() {
	this.initialize(ss["Map_HTML5 Canvas_atlas_1"]);
	this.gotoAndStop(0);
}).prototype = p = new cjs.Sprite();



(lib.Water = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("bulkbulknulavoda32559");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f("#0066CC").s().p("AO+XwQg0gXgfgtIgdgxQgSgcgTgMQgRgKgbgEQgPgCgggBQhVgHhLgrQhMgsgxhGQgwhFgNhXQgOhWAYhSQAahGAKgjQAUg9gLgsQidAjidgNQhvgIhSgpQhigxgghVQgshzBciwQAxhZAWguQAlhPAFhAQhZAFhNhCQhJg/gXhcQgVhVAUhhQAShVAwhXQg8gFi1AWQiWAShXgaQhMgWg8g4Qg8g4gchKIghh8QgThKghgmQgVgZgwggQg2gkgTgTQgkgjgPgxQgQgyAKgxQAJgxAignQAigoAwgQQA0gSBAAOQA2ALA5AhQB6BGBRB5QBQB6ARCLIEHgVQB5gKBFAIQBpALBBA0QA1AqAfBGQAcA/AEBMQAEA/gNBPQgHAugVBeQBeAWBBBYQA9BSALBpQAKBagYBrQgSBPgtBwQBkgwA/gPQBdgXBIAaQCVA2A3EDQAeCGgDBpQgDCDg0BhQC/AVByBQQBGAxApBHQArBLgCBOQgCBOgxA+QgyBBhJAMQgPACgOAAQgnAAglgRg");
	this.shape.setTransform(-0.0011,0.0254);
	this.shape._off = true;

	this.timeline.addTween(cjs.Tween.get(this.shape).wait(3).to({_off:false},0).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-123.7,-153.7,247.4,307.5);


(lib.MetroButton = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("z_uk1ostorozhnod_erizakry_ayutsya1");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AEdAAQAAB2hTBUQhUBTh2AAQh1AAhUhTQhThUAAh2QAAh1BThUQBUhTB1AAQB2AABUBTQBTBUAAB1g");
	this.shape.setTransform(-0.5,-0.45);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#0066CC").s().p("AjIDKQhUhUAAh2QAAh1BUhUQBThTB1AAQB2AABTBTQBUBUAAB1QAAB2hUBUQhTBTh2AAQh1AAhThTg");
	this.shape_1.setTransform(-0.5,-0.45);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape_1},{t:this.shape}]},3).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-30,-29.9,59,59);


(lib.Forest = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("flamingo");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AHRmOQAABuhNBOQhOBOhuAAQhuAAhNhOQhNhOAAhuQAAhuBNhNQBNhOBuAAQBuAABOBOQBNBNAABugAiYCvQgcAHggAAQhoAAhKhKQhKhKAAhoQAAhoBKhLQBKhJBoAAQBpAABKBJQBJBLAABoQAABohJBKQgoAogyASQgOAGgPADgAh7CmQAdgGAfAAQBoAABKBJQBKBKAABpQAABohKBKQhKBKhoAAQhoAAhKhKQhKhKAAhoQAAhpBKhKQAogoAxgS");
	this.shape.setTransform(23.5,7.4);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#0066CC").s().p("AjxJNQhJhJAAhpQAAhoBJhKQAogoAxgSQgcAGggABQhogBhKhJQhKhLAAhnQAAhoBKhLQBKhJBoAAQBpAABKBJQBJBLAABoQAABnhJBLQgoAogxASQgPAGgPADQAPgDAPgGQAcgHAgAAQBnABBJBJQBLBKAABoQAABphLBJQhJBLhngBQhpABhKhLgAANjSQhNhNAAhvQAAhtBNhOQBNhOBuAAQBuAABOBOQBNBOAABtQAABvhNBNQhOBOhuAAQhuAAhNhOg");
	this.shape_1.setTransform(23.5,7.4);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape_1},{t:this.shape}]},3).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-24,-59.9,95,134.7);


// stage content:
(lib.Map_HTML5Canvas = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// Слой_1
	this.instance = new lib.Water();
	this.instance.setTransform(149.8,94.75,0.7806,0.7724,0,0,-8.3237);
	new cjs.ButtonHelper(this.instance, 0, 1, 2, false, new lib.Water(), 3);

	this.instance_1 = new lib.Forest();
	this.instance_1.setTransform(470.75,153.75);
	new cjs.ButtonHelper(this.instance_1, 0, 1, 2, false, new lib.Forest(), 3);

	this.instance_2 = new lib.MetroButton();
	this.instance_2.setTransform(314.5,315.45,0.6207,0.6207);
	new cjs.ButtonHelper(this.instance_2, 0, 1, 2, false, new lib.MetroButton(), 3);

	this.instance_3 = new lib.карта_Минска();
	this.instance_3.setTransform(-61,0,0.7012,0.7012);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_3},{t:this.instance_2},{t:this.instance_1},{t:this.instance}]}).wait(1));

	this._renderFirstFrame();

}).prototype = p = new lib.AnMovieClip();
p.nominalBounds = new cjs.Rectangle(259,227.7,407.20000000000005,251.3);
// library properties:
lib.properties = {
	id: 'AE7F331D1EFFAD4E8B7BB0FBB3E1C218',
	width: 640,
	height: 480,
	fps: 60,
	color: "#FFFFFF",
	opacity: 1.00,
	manifest: [
		{src:"images/Map_HTML5 Canvas_atlas_1.png?1677073967538", id:"Map_HTML5 Canvas_atlas_1"},
		{src:"sounds/bulkbulknulavoda32559.mp3?1677073967563", id:"bulkbulknulavoda32559"},
		{src:"sounds/flamingo.mp3?1677073967563", id:"flamingo"},
		{src:"sounds/z_uk1ostorozhnod_erizakry_ayutsya1.mp3?1677073967563", id:"z_uk1ostorozhnod_erizakry_ayutsya1"}
	],
	preloads: []
};



// bootstrap callback support:

(lib.Stage = function(canvas) {
	createjs.Stage.call(this, canvas);
}).prototype = p = new createjs.Stage();

p.setAutoPlay = function(autoPlay) {
	this.tickEnabled = autoPlay;
}
p.play = function() { this.tickEnabled = true; this.getChildAt(0).gotoAndPlay(this.getTimelinePosition()) }
p.stop = function(ms) { if(ms) this.seek(ms); this.tickEnabled = false; }
p.seek = function(ms) { this.tickEnabled = true; this.getChildAt(0).gotoAndStop(lib.properties.fps * ms / 1000); }
p.getDuration = function() { return this.getChildAt(0).totalFrames / lib.properties.fps * 1000; }

p.getTimelinePosition = function() { return this.getChildAt(0).currentFrame / lib.properties.fps * 1000; }

an.bootcompsLoaded = an.bootcompsLoaded || [];
if(!an.bootstrapListeners) {
	an.bootstrapListeners=[];
}

an.bootstrapCallback=function(fnCallback) {
	an.bootstrapListeners.push(fnCallback);
	if(an.bootcompsLoaded.length > 0) {
		for(var i=0; i<an.bootcompsLoaded.length; ++i) {
			fnCallback(an.bootcompsLoaded[i]);
		}
	}
};

an.compositions = an.compositions || {};
an.compositions['AE7F331D1EFFAD4E8B7BB0FBB3E1C218'] = {
	getStage: function() { return exportRoot.stage; },
	getLibrary: function() { return lib; },
	getSpriteSheet: function() { return ss; },
	getImages: function() { return img; }
};

an.compositionLoaded = function(id) {
	an.bootcompsLoaded.push(id);
	for(var j=0; j<an.bootstrapListeners.length; j++) {
		an.bootstrapListeners[j](id);
	}
}

an.getComposition = function(id) {
	return an.compositions[id];
}


an.makeResponsive = function(isResp, respDim, isScale, scaleType, domContainers) {		
	var lastW, lastH, lastS=1;		
	window.addEventListener('resize', resizeCanvas);		
	resizeCanvas();		
	function resizeCanvas() {			
		var w = lib.properties.width, h = lib.properties.height;			
		var iw = window.innerWidth, ih=window.innerHeight;			
		var pRatio = window.devicePixelRatio || 1, xRatio=iw/w, yRatio=ih/h, sRatio=1;			
		if(isResp) {                
			if((respDim=='width'&&lastW==iw) || (respDim=='height'&&lastH==ih)) {                    
				sRatio = lastS;                
			}				
			else if(!isScale) {					
				if(iw<w || ih<h)						
					sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==1) {					
				sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==2) {					
				sRatio = Math.max(xRatio, yRatio);				
			}			
		}
		domContainers[0].width = w * pRatio * sRatio;			
		domContainers[0].height = h * pRatio * sRatio;
		domContainers.forEach(function(container) {				
			container.style.width = w * sRatio + 'px';				
			container.style.height = h * sRatio + 'px';			
		});
		stage.scaleX = pRatio*sRatio;			
		stage.scaleY = pRatio*sRatio;
		lastW = iw; lastH = ih; lastS = sRatio;            
		stage.tickOnUpdate = false;            
		stage.update();            
		stage.tickOnUpdate = true;		
	}
}
an.handleSoundStreamOnTick = function(event) {
	if(!event.paused){
		var stageChild = stage.getChildAt(0);
		if(!stageChild.paused || stageChild.ignorePause){
			stageChild.syncStreamSounds();
		}
	}
}
an.handleFilterCache = function(event) {
	if(!event.paused){
		var target = event.target;
		if(target){
			if(target.filterCacheList){
				for(var index = 0; index < target.filterCacheList.length ; index++){
					var cacheInst = target.filterCacheList[index];
					if((cacheInst.startFrame <= target.currentFrame) && (target.currentFrame <= cacheInst.endFrame)){
						cacheInst.instance.cache(cacheInst.x, cacheInst.y, cacheInst.w, cacheInst.h);
					}
				}
			}
		}
	}
}


})(createjs = createjs||{}, AdobeAn = AdobeAn||{});
var createjs, AdobeAn;