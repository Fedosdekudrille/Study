(function (cjs, an) {

var p; // shortcut to reference prototypes
var lib={};var ss={};var img={};
lib.ssMetadata = [];


(lib.AnMovieClip = function(){
	this.actionFrames = [];
	this.ignorePause = false;
	this.currentSoundStreamInMovieclip;
	this.soundStreamDuration = new Map();
	this.streamSoundSymbolsList = [];

	this.gotoAndPlayForStreamSoundSync = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.gotoAndPlay = function(positionOrLabel){
		this.clearAllSoundStreams();
		var pos = this.timeline.resolve(positionOrLabel);
		if (pos != null) { this.startStreamSoundsForTargetedFrame(pos); }
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.play = function(){
		this.clearAllSoundStreams();
		this.startStreamSoundsForTargetedFrame(this.currentFrame);
		cjs.MovieClip.prototype.play.call(this);
	}
	this.gotoAndStop = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndStop.call(this,positionOrLabel);
		this.clearAllSoundStreams();
	}
	this.stop = function(){
		cjs.MovieClip.prototype.stop.call(this);
		this.clearAllSoundStreams();
	}
	this.startStreamSoundsForTargetedFrame = function(targetFrame){
		for(var index=0; index<this.streamSoundSymbolsList.length; index++){
			if(index <= targetFrame && this.streamSoundSymbolsList[index] != undefined){
				for(var i=0; i<this.streamSoundSymbolsList[index].length; i++){
					var sound = this.streamSoundSymbolsList[index][i];
					if(sound.endFrame > targetFrame){
						var targetPosition = Math.abs((((targetFrame - sound.startFrame)/lib.properties.fps) * 1000));
						var instance = playSound(sound.id);
						var remainingLoop = 0;
						if(sound.offset){
							targetPosition = targetPosition + sound.offset;
						}
						else if(sound.loop > 1){
							var loop = targetPosition /instance.duration;
							remainingLoop = Math.floor(sound.loop - loop);
							if(targetPosition == 0){ remainingLoop -= 1; }
							targetPosition = targetPosition % instance.duration;
						}
						instance.loop = remainingLoop;
						instance.position = Math.round(targetPosition);
						this.InsertIntoSoundStreamData(instance, sound.startFrame, sound.endFrame, sound.loop , sound.offset);
					}
				}
			}
		}
	}
	this.InsertIntoSoundStreamData = function(soundInstance, startIndex, endIndex, loopValue, offsetValue){ 
 		this.soundStreamDuration.set({instance:soundInstance}, {start: startIndex, end:endIndex, loop:loopValue, offset:offsetValue});
	}
	this.clearAllSoundStreams = function(){
		this.soundStreamDuration.forEach(function(value,key){
			key.instance.stop();
		});
 		this.soundStreamDuration.clear();
		this.currentSoundStreamInMovieclip = undefined;
	}
	this.stopSoundStreams = function(currentFrame){
		if(this.soundStreamDuration.size > 0){
			var _this = this;
			this.soundStreamDuration.forEach(function(value,key,arr){
				if((value.end) == currentFrame){
					key.instance.stop();
					if(_this.currentSoundStreamInMovieclip == key) { _this.currentSoundStreamInMovieclip = undefined; }
					arr.delete(key);
				}
			});
		}
	}

	this.computeCurrentSoundStreamInstance = function(currentFrame){
		if(this.currentSoundStreamInMovieclip == undefined){
			var _this = this;
			if(this.soundStreamDuration.size > 0){
				var maxDuration = 0;
				this.soundStreamDuration.forEach(function(value,key){
					if(value.end > maxDuration){
						maxDuration = value.end;
						_this.currentSoundStreamInMovieclip = key;
					}
				});
			}
		}
	}
	this.getDesiredFrame = function(currentFrame, calculatedDesiredFrame){
		for(var frameIndex in this.actionFrames){
			if((frameIndex > currentFrame) && (frameIndex < calculatedDesiredFrame)){
				return frameIndex;
			}
		}
		return calculatedDesiredFrame;
	}

	this.syncStreamSounds = function(){
		this.stopSoundStreams(this.currentFrame);
		this.computeCurrentSoundStreamInstance(this.currentFrame);
		if(this.currentSoundStreamInMovieclip != undefined){
			var soundInstance = this.currentSoundStreamInMovieclip.instance;
			if(soundInstance.position != 0){
				var soundValue = this.soundStreamDuration.get(this.currentSoundStreamInMovieclip);
				var soundPosition = (soundValue.offset?(soundInstance.position - soundValue.offset): soundInstance.position);
				var calculatedDesiredFrame = (soundValue.start)+((soundPosition/1000) * lib.properties.fps);
				if(soundValue.loop > 1){
					calculatedDesiredFrame +=(((((soundValue.loop - soundInstance.loop -1)*soundInstance.duration)) / 1000) * lib.properties.fps);
				}
				calculatedDesiredFrame = Math.floor(calculatedDesiredFrame);
				var deltaFrame = calculatedDesiredFrame - this.currentFrame;
				if((deltaFrame >= 0) && this.ignorePause){
					cjs.MovieClip.prototype.play.call(this);
					this.ignorePause = false;
				}
				else if(deltaFrame >= 2){
					this.gotoAndPlayForStreamSoundSync(this.getDesiredFrame(this.currentFrame,calculatedDesiredFrame));
				}
				else if(deltaFrame <= -2){
					cjs.MovieClip.prototype.stop.call(this);
					this.ignorePause = true;
				}
			}
		}
	}
}).prototype = p = new cjs.MovieClip();
// symbols:



(lib.ToStart = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(0.1,1,1).p("AAAAKIAAJYIpNpYIJNprIAAJrIJOprIAATDg");
	this.shape.setTransform(1,-1.975);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#FFFF00").s().p("AAAAKIJOprIAATDgApNAKIJNprIAAJrIAAJYg");
	this.shape_1.setTransform(1,-1.975);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f().s("#000000").ss(0.1,1,1).p("AAuppIAFgFIAAAFIAAS9QlXkljllDIAAAAII3pVQADAAACAAQD4gVFWAQIAATCIpOAAQvTC2GXseIgNANIANgNQkuoLNlhKgAoagDIAEgE");
	this.shape_2.setTransform(-4.1498,-0.6259);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f("#FFFF00").s().p("AoJgUQDlFDFXElQlXkljllDIAAAAII3pVIAFAAIgFAAIAFgFIAAAFQD4gVFXAQIAATCIpPAAIAAy9IAAS9Qi0AiiGAAQpOAAFMqKgAoJgUIAAAAIgNANgAoJgUgAAuppIo3JVQkuoLNlhKg");
	this.shape_3.setTransform(-4.1498,-0.6259);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_1},{t:this.shape}]}).to({state:[{t:this.shape_3},{t:this.shape_2}]},3).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-69.3,-64.6,130.3,128);


(lib.StartButton = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(0.1,1,1).p("ArKtvIWVONI2VNSg");
	this.shape.setTransform(0.525,-2.95);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#00FF00").s().p("ArKtvIWVONI2VNSg");
	this.shape_1.setTransform(0.525,-2.95);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f().s("#000000").ss(0.1,1,1).p("At7umIYqAAUAMqAkUglUgI1g");
	this.shape_2.setTransform(18.2629,2.576);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f("#00FF00").s().p("At7M5IAA7fIYqAAQKLdN2JAAQlZAAnThug");
	this.shape_3.setTransform(18.2629,2.576);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_1},{t:this.shape}]}).to({state:[{t:this.shape_3},{t:this.shape_2}]},3).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-71.9,-91.9,180.4,189);


(lib.Paw = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f("#663300").s().p("ArfFsQgIgCgBgHQgBgFADgHQAyiAA0hPQBGhoBcguQAegPAogNQAYgHAwgNQA5gOAeABQAjADA1AZQBBAfAVAGQA3APBOgLQB2gQBqg7QBrg6BOhaIAZgdQAPgQAOgJQAOgIAZgJICkg9QAQgFAEAGQAEAFgEAGQgDAGgGAEQg3ArhHARIgiAIQgTAGgNAHQgPAJgPASIgZAfQgVAYgvAnQgnAigXARQgjAaggARQglATgxAQQggAIg6AOQhJARgkABQhJAAhSgnIgpgUQgXgLgUgCQgWgEgcAEQgSADggAJIhJAVQgoAMgdAQQhQAsg9BcQgtBFgsBwQgHATgJAFQgEACgFAAIgFAAg");
	this.shape.setTransform(0.0442,-2.9702);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#663300").s().p("ArqDOQgHgDAAgGQAAgGAEgFQBFhuA/hAQBThTBegbQAegJAogFIBGgFQA3gDAbAGQAhAIAsAhQA3ApATAJQAwAXBKADQBwAGBtgjQBtgjBYhGIAcgWQARgMAPgGQAOgEAZgFICjgbQAPgCADAGQADAFgFAGQgEAEgGADQg7AehEADIgiACQgSACgNAEQgPAGgSAOIgdAYQgXATgyAcQgrAYgYAMQglARghAKQglALgwAGQggADg4ACQhHADghgFQhEgNhFgyIgigaQgUgOgSgGQgUgIgagBQgRAAgfACQgwAEgYADQgnAFgeAKQhSAahJBKQg2A4g9BgQgJAQgJADIgGABQgDAAgFgCg");
	this.shape_1.setTransform(-1.015,-18.3347);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#663300").s().p("ACyB7QgfgDg2gKQhEgLgfgMQg+gag4g+IgcgfQgRgSgQgIQgRgMgagGQgQgEgegEIhGgIQgmgDgfAEQhUAJhVA5Qg/AqhNBQQgMANgKACQgGABgHgFQgGgEACgGQABgFAFgFQBYhaBIgxQBhhBBfgHQAfgCAnADIBEAIQA2AJAZALQAdAPAkAoQAtAxAQAMQApAhBHASQBqAbBvgMQBwgMBjgxIAfgQQATgIAOgDQAQgBAYABICiAFQAPABABAGQACAGgGAFQgFADgGABQg+AShCgLIghgFQgSgCgOACQgPADgUAKIggARQgaANg2ARQguAPgZAGQgnAKgiAEIgiABQgYAAgbgCg");
	this.shape_2.setTransform(-2.0883,-39.6128);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f("#663300").s().p("AJwCwIgggMQgRgFgOgCQgQAAgWAGIgkAKQgdAIg5AGQgxAFgbABQgpACgigDQgngFgugOQgegJg1gVQhCgZgbgTQg6gngrhJIgVglQgNgVgOgMQgPgQgYgLQgQgHgdgKIhFgXQglgLgggDQhVgHhhAnQhIAeheBAQgPALgKAAQgGgBgGgGQgFgGADgFQACgFAGgEQBqhJBTgiQBugtBhAOQAfADAnAMQAWAGAsAQQA0ATAXARQAaAVAcAwQAiA5AOAQQAiApBDAhQBkAxByAMQBzALBsgdIAjgKQAUgEAQAAQAPACAYAGIChAoQAPADgBAHQABAGgGADQgGADgGAAIgVAAQg3AAg2gUg");
	this.shape_3.setTransform(-3.1647,-56.4222);

	this.shape_4 = new cjs.Shape();
	this.shape_4.graphics.f("#663300").s().p("AL4FDQhGgJg+gmIgegSQgRgJgPgFQgQgDgYACIgoADQgfADg9gFQg0gFgcgEQgrgGgjgKQgngNgugYQgdgPgzggQhAgogZgZQg0g0gehVIgPgrQgJgYgMgPQgNgUgXgQQgOgKgdgRQgsgZgXgMQglgTgggJQhXgZhtAWQhRAQhuAxQgSAIgKgCQgHgCgFgIQgEgHAEgFQADgFAIgCQB9g3BdgTQB7gZBiAhQAgAKAmAUQAWALAqAZQAzAfAUAVQAXAbAUA4QAYBEALATQAbAwA/AwQBfBHB1AjQB1AiB2gJIAmgDQAWAAAQADQAQAFAYALICfBJQAPAHgCAHQAAAHgHACIgHABIgGgBg");
	this.shape_4.setTransform(-4.2382,-69.0845);

	this.shape_5 = new cjs.Shape();
	this.shape_5.graphics.f("#663300").s().p("AJxDEIgfgNQgSgHgOgCQgQgBgWAGIglAJQgdAGg6AEQgyADgaAAQgqABgjgFQgmgHgugPQgegLg1gXQhBgcgbgUQg5gpgohMIgUgmQgMgWgOgNQgPgQgYgMQgPgIgdgLQgtgSgXgIQgmgMgggFQhVgLhjAkQhKAbhhA9QgQAKgKAAQgGgBgGgGQgFgGADgGQADgFAGgDQBuhGBVgfQBxgoBhARQAfAFAmANQAXAIArARQA0AWAWARQAaAWAaAyQAgA8ANAQQAhAqBCAlQBjA1BzAQQBzAPBvgYIAjgIQAVgEAPABQAQACAYAHICgAvQAPAEgBAGQABAHgGADQgGACgHAAIgJAAQg+AAg7gZg");
	this.shape_5.setTransform(-3.4031,-58.9444);

	this.shape_6 = new cjs.Shape();
	this.shape_6.graphics.f("#663300").s().p("ACtCPQgfgFg2gOQhDgRgdgPQg9gfgyhCIgagiQgPgTgPgKQgQgOgZgHQgQgFgegHIhFgOQgngGgfABQhUAChaAzQhCAlhUBJQgOAMgJABQgHAAgGgFQgFgFACgGQABgFAGgEQBfhTBNgrQBmg5BfACQAfgBAnAHQAXAEAsAKQA2ANAYANQAcARAhAsQAoA0APAOQAmAkBFAYQBoAkBxgDQBxgDBmgpIAhgNQATgHAPgBQAQgBAYADIChAUQAPACABAGQABAGgGAEQgFADgGABQhAAMhBgQIgggIQgSgDgOAAQgQACgUAIIgiAPQgbALg4ANQguAKgaAFQgoAGgiABQgmAAgvgIg");
	this.shape_6.setTransform(-2.519,-46.3769);

	this.shape_7 = new cjs.Shape();
	this.shape_7.graphics.f("#663300").s().p("ArwCDQgHgEABgFQABgGAFgFQBQhiBEg2QBchJBegPQAfgFAnAAQAXAAAuADQA2AEAaAJQAfAMAnAmQAxAuARALQAsAcBIAMQBsATBvgWQBvgVBeg5IAegSQASgJAPgFQAPgCAYgCICigHQAQAAABAGQADAGgFAEQgFAEgGACQg9AWhDgFIghgCQgSAAgOADQgPAEgTALIgfAUQgZAPg0AVQgtASgYAJQgnANghAGQgmAHgvgBQgfAAg4gFQhFgFgfgKQhBgVg9g5IgfgdQgRgQgRgHQgTgLgagEQgQgCgfgCIhGgCQgnAAgfAHQhSAQhQBAQg8AvhGBXQgMAOgJACIgDABQgFAAgFgEg");
	this.shape_7.setTransform(-1.665,-29.5312);

	this.shape_8 = new cjs.Shape();
	this.shape_8.graphics.f("#663300").s().p("AroDuQgHgDAAgGQgBgGAEgGQBBhxA9hDQBRhXBdgfQAegKAogGQAXgEAvgFQA4gFAcAFQAhAHAuAgQA5AmATAJQAxAVBLAAQBxABBtgmQBtgoBWhKIAbgYQARgMAOgHQAPgFAYgFICjgjQAQgDADAHQADAFgFAGQgEAEgFADQg7AhhEAGIgiADQgTADgNAFQgPAGgRAPIgcAZQgXAUgyAeQgpAagYANQglAUghALQglAMgwAIQggAEg5AFQhGAFgjgEQhEgKhHgwIgkgZQgVgNgSgFQgUgHgbAAQgRAAggAEIhHAJQgoAHgeALQhRAehHBNQg0A7g5BjQgKARgIAEIgFABQgFAAgEgCg");
	this.shape_8.setTransform(-0.7928,-15.2824);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape}]}).to({state:[{t:this.shape_1}]},1).to({state:[{t:this.shape_2}]},1).to({state:[{t:this.shape_3}]},1).to({state:[{t:this.shape_4}]},1).to({state:[{t:this.shape_5}]},1).to({state:[{t:this.shape_6}]},1).to({state:[{t:this.shape_7}]},1).to({state:[{t:this.shape_8}]},1).to({state:[{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-82.3,-101.4,156.89999999999998,134.9);


(lib.PauseButton = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f("#000000").s().p("ABaEXIAAotIDMAAIAAItgAklEXIAAotIDMAAIAAItg");
	this.shape.setTransform(0,2.625);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#FFFFFF").ss(1,1,1).p("AsapDIY1AAIAASHI41AAg");
	this.shape_1.setTransform(3.5,3.05);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#000000").s().p("AsaJEIAAyHIY1AAIAASHg");
	this.shape_2.setTransform(3.5,3.05);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape,p:{x:0,y:2.625}}]}).to({state:[{t:this.shape,p:{x:0.55,y:1.575}}]},1).to({state:[{t:this.shape,p:{x:0.55,y:1.575}}]},1).to({state:[{t:this.shape_2},{t:this.shape_1}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-77,-55.9,161,118);


(lib.BagBodyBack = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(5,1,1).p("Aon0nQJNE0ICk0IAHAAAqot0QKmFcKllcAp3J6QJ3CLJ3iLQAAAAABAAAj8UoQEkjIDUDIIAdAAArhlqQLhDaLijaArSCKQK8DbLkjbAn4PNQH4B4H4h3");
	this.shape.setTransform(74.025,252.05);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#663300").ss(1,1,1).p("AEEhJQg7BQhzBDAkCg/QA6BKBsA/");
	this.shape_1.setTransform(73.35,51.25);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f().s("#000000").ss(1,1,1).p("AD6jfQCTD+iGC3Aj5jfQiXEFCRC6");
	this.shape_2.setTransform(72.975,22.425);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f().s("#000000").ss(0.1,1,1).p("Akz0yQgehAAAhNQAAh2BIhZQAMgPAOgPQBihiCLAAQCLAABiBiQAKAKAIAKQBQBcAAB9QAABLgcA/QgZA2gtAtQhiBiiLAAQgHAAgIAAQiBgFhdhdQgsgsgYg0gAJ4QEQgtCdhACNQgJAVgKAUQhyDtiJBuQh2BdiHAAQiGAAh2hdQiKhuhyjtQgJgUgKgVQg/iNgtidQhBjmgakKQgRivAAi/QAAhFAChDQAJkWAwjzQAujqBTjJQANgiAPghQBjjaB1h3AEx01QB2B3BkDdQAPAhAOAiQCnGaATIiQACBDAABFQAAHhhsF8");
	this.shape_3.setTransform(74,212.7);

	this.shape_4 = new cjs.Shape();
	this.shape_4.graphics.f("#000000").s().p("Aj7ADQEjjHDVDHQh2BeiHAAQiGAAh1heg");
	this.shape_4.setTransform(74,383.7);

	this.shape_5 = new cjs.Shape();
	this.shape_5.graphics.f("#330000").s().p("Aj7X0QiLhuhxjtQD8A8D8AAIABAAIAAAAQD7AAD7g7QhyDtiJBtQjVjIkjDIgAABTVQj8AAj8g8IgTgpQhAiMgsieQJ2CKJ4iKQgtCehACMIgTAqQj7A7j7AAIAAAAIgBAAgAH4SaIAAAAgAn3SZIAAAAgAp2NGQhCjmgakKQFeBtFpAAQFnAAFyhtQlyBtlnAAQlpAAlehtQgRivAAi9QAAhFADhDQAIkXAwjzQAvjqBSjJQANgiAQghQBijaB2h3QAXA0AsAsQBdBcCCAGIAOAAQCLAABihiQAtguAZg2QB2B3BkDeIAdBDQCoGaASIjQACBDAABFQAAHfhsF9IAAAAQk8BFk8AAQk7AAk7hFgALiieQlxBtlxAAQlwAAlwhtQFwBtFwAAQFxAAFxhtgAgCn6QFSAAFTiuQlTCulSAAQlTAAlTiuQFTCuFTAAgAIoxbQkBCakUAAQkTAAkniaQEnCaETAAQEUAAEBiagAp2NGIAAAAgArSFWIAAAAg");
	this.shape_5.setTransform(74,231.675);

	this.shape_6 = new cjs.Shape();
	this.shape_6.graphics.f("#663300").s().p("AgOFPQiCgFhchdQgsgsgYg0QgehBAAhMQAAh1BIhZQAMgQAOgOQBihiCKAAQCLAABiBiIASAUQBQBcAAB8QAABKgcA/QgZA2gtAuQhiBiiLAAIgOAAgABShEQByhDA7hRQg7BRhyBDgAhhhEQhrg/g6hLQA6BLBrA/g");
	this.shape_6.setTransform(73.75,65.55);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_6},{t:this.shape_5},{t:this.shape_4},{t:this.shape_3},{t:this.shape_2},{t:this.shape_1},{t:this.shape}]}).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-2.2,-1,152.5,395.4);


(lib.BagBody = function(mode,startPosition,loop,reversed) {
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
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AierFQAAAygkAkQgkAkgyAAQgzAAgkgkQgjgkAAgyQAAgzAjgkQAkgjAzAAQAyAAAkAjQAkAkAAAzgAEn+uQCTEAiGC3Qg7BRhzBDAFZnRQAAAyAkAkQAkAkAyAAQACAAACAAQAwgCAjgiQAjgkAAgyQAAgzgjgkQgjgigwgBQgCAAgCAAQgyAAgkAjQgkAkAAAzgAC1EZQAAAyAkAkQAkAkAyAAQAzAAAkgkQAjgkAAgyQAAgzgjgkQgkgjgzAAQgyAAgkAjQgkAkAAAzgAAEVKQAAAygjAkQgjAkgzAAQgIAAgIgBQgogFgegeQgkgkAAgyQAAgzAkgkQAkgjAyAAQAkAAAdASQALAHAKAKQAjAkAAAzgAC1SmQAAAzAkAjQAkAkAyAAQAIAAAIgBQApgFAegeQAjgjAAgzQAAgygjgkQgkgkgzAAQgkAAgcASQgMAIgKAKQgkAkAAAygAAlvPMAASAt+AlYC5QAAAygkAkQgkAkgyAAQgzAAgkgkQgjgkAAgyQAAgzAjgkQAkgjAzAAQAyAAAkAjQAkAkAAAzgAjN+uQiXEGCRC6QA7BMBsA/");
	this.shape.setTransform(19.525,-32.675);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f().s("#000000").ss(0.1,1,1).p("Akz0yQgehAAAhNQAAh2BIhZQAMgPAOgPQBihiCLAAQCLAABiBiQAJAKAJAKQBQBcAAB9QAABLgdA/QgYA2gtAtQhiBiiLAAQgHAAgIAAQiBgFhdhdQgrgrgZg1gADK6YQAAAPgMALQgKALgQAAQgPAAgLgLQgMgLAAgPQAAgQAMgKQALgMAPAAQAQAAAKAMQAMAKAAAQgAiE6YQAAAPgLALQgLALgQAAQgPAAgLgLQgLgLAAgPQAAgQALgKQALgMAPAAQAQAAALAMQALAKAAAQgAEw01QB3B3BkDdQDZHhAAKnQAAKnjZHgQjYHhkzAAQkyAAjZnhQjYngAAqnQAAqnDYnhQBjjaB1h3");
	this.shape_1.setTransform(25,-16.65);

	this.shape_2 = new cjs.Shape();
	this.shape_2.graphics.f("#FF6600").s().p("AiFSBQgogFgegeQgkgjAAgzQAAgyAkglQAkgjAyAAQAkAAAdASQALAHAKAKQAjAlAAAyQAAAzgjAjQgjAkgzAAIgQgBgADZO6QgkgjAAgzQAAgyAkgkQAKgKAMgHQAcgSAkAAQAzAAAkAjQAjAkAAAyQAAAzgjAjQgeAfgpAFIgQAAQgyABgkglgADZAtQgkgjAAgyQAAgzAkgjQAkgkAyAAQAzAAAkAkQAjAjAAAzQAAAygjAjQgkAkgzAAQgyAAgkgkgAopgyQgjgkAAgyQAAgzAjgkQAkgjAzAAQAyAAAkAjQAkAkAAAzQAAAygkAkQgkAkgyAAQgzAAgkgkgAHTqZQgyAAgkgkQgkgjAAgzQAAgzAkgjQAkgkAyAAIAEAAQAwABAjAjQAjAjAAAzQAAAzgjAjQgjAigwACIgEAAgAlvuxQgjgjAAgzQAAgzAjgjQAkgkAzAAQAyAAAkAkQAkAjAAAzQAAAzgkAjQgkAkgyAAQgzAAgkgkg");
	this.shape_2.setTransform(19.525,-0.5);

	this.shape_3 = new cjs.Shape();
	this.shape_3.graphics.f("#000000").s().p("ACNAbQgLgMAAgPQAAgOALgLQALgLAPAAQAQAAALALQALALAAAOQAAAPgLAMQgLALgQgBQgPABgLgLgAjBAbQgLgMAAgPQAAgOALgLQALgLAQAAQAPAAALALQALALAAAOQAAAPgLAMQgLALgPgBQgQABgLgLg");
	this.shape_3.setTransform(24.675,-185.55);

	this.shape_4 = new cjs.Shape();
	this.shape_4.graphics.f("#663300").s().p("AAAcOMgARgt+MAARAt+QkyABjZnhQjYngAAqnQAAqnDYnhQBjjaB1h3QAZA1AsArQBcBdCBAFIAPAAQCKABBihjQAugtAYg2QgYA2guAtQhiBjiKgBIgPAAQiBgFhchdQgsgrgZg1QgdhAAAhNQAAh2BHhZQAMgPAPgOQBihjCKABQCKgBBiBjIATAUQBQBbgBB9QABBLgdA/QB3B3BkDdQDZHhAAKnQAAKnjZHgQjZHhkygBIAAAAgAkCRTQgkAkABAyQgBAzAkAkQAeAdAoAGIAQABQAzgBAkgjQAjgkABgzQgBgygjgkQgKgKgMgHQgcgTglABQgygBgkAkgAC4OeQgMAIgKAJQgjAlgBAyQABAyAjAkQAkAkAzAAIAQgBQAogFAegeQAkgkgBgyQABgygkglQgkgjgyAAQglAAgcASgACiAiQgjAkgBAzQABAyAjAjQAkAkAzAAQAyAAAkgkQAkgjgBgyQABgzgkgkQgkgjgyAAQgzAAgkAjgApfg9QgkAkAAAxQAAAzAkAkQAjAjAzABQAzgBAjgjQAkgkAAgzQAAgxgkgkQgjgkgzABQgzgBgjAkgAFHrIQgkAkAAAzQAAAyAkAjQAjAkAyAAIAEAAQAxgBAigjQAkgjAAgyQAAgzgkgkQgigjgxgBIgEAAQgyAAgjAkgAmmu8QgjAkAAAzQAAAyAjAjQAlAkAyAAQAyAAAkgkQAkgjAAgyQAAgzgkgkQgkgkgyAAQgyAAglAkgABP4EQBzhCA7hRQg7BRhzBCgAhj4EQhrg+g7hMQA7BMBrA+gACK6yQgMALAAAPQAAAPAMAMQAKALAQgBQAQABAKgLQAMgMAAgPQAAgPgMgLQgKgLgQAAQgQAAgKALgAjE6yQgLALAAAPQAAAPALAMQALALAPgBQAQABALgLQALgMAAgPQAAgPgLgLQgLgLgQAAQgPAAgLALg");
	this.shape_4.setTransform(25,-16.65);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.shape_4},{t:this.shape_3},{t:this.shape_2},{t:this.shape_1},{t:this.shape}]}).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-50,-230.3,150,395.3);


(lib.Bag = function(mode,startPosition,loop,reversed) {
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
	this.instance = new lib.Paw("synched",4);
	this.instance.setTransform(107.6,-7.6,0.6303,0.6303,0,-14.9991,165.0009,-4,-34.1);

	this.instance_1 = new lib.Paw("synched",0);
	this.instance_1.setTransform(89.75,139.45,0.6303,0.6303,-135.002,0,0,-4,-34.2);

	this.instance_2 = new lib.Paw("synched",0);
	this.instance_2.setTransform(-102,139.45,0.6303,0.6303,0,135.002,-44.998,-4,-34.2);

	this.instance_3 = new lib.Paw("synched",2);
	this.instance_3.setTransform(-120.75,-2.55,0.6303,0.6303,14.9991,0,0,-4,-34.1);

	this.instance_4 = new lib.Paw("synched",4);
	this.instance_4.setTransform(92.7,-99,0.6303,0.6303,0,-14.9991,165.0009,-4,-34.1);

	this.instance_5 = new lib.Paw("synched",0);
	this.instance_5.setTransform(-104.95,-99,0.6303,0.6303,14.9991,0,0,-4,-34.1);

	this.instance_6 = new lib.BagBody("synched",0);
	this.instance_6.setTransform(-6,-10.95,1,1,0,0,0,25,-32.7);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_6},{t:this.instance_5,p:{startPosition:0}},{t:this.instance_4,p:{startPosition:4}},{t:this.instance_3,p:{startPosition:2}},{t:this.instance_2,p:{startPosition:0}},{t:this.instance_1,p:{startPosition:0}},{t:this.instance,p:{x:107.6,y:-7.6}}]}).to({state:[{t:this.instance_6},{t:this.instance_5,p:{startPosition:9}},{t:this.instance_4,p:{startPosition:3}},{t:this.instance_3,p:{startPosition:1}},{t:this.instance_2,p:{startPosition:9}},{t:this.instance_1,p:{startPosition:9}},{t:this.instance,p:{x:109.55,y:-9.2}}]},9).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-174.5,-208.6,335.9,408.79999999999995);


(lib.BagBack = function(mode,startPosition,loop,reversed) {
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
	this.instance = new lib.BagBodyBack("synched",0);
	this.instance.setTransform(-18.05,-37.1,1,1,0,0,0,74,196.5);

	this.instance_1 = new lib.Bag();
	this.instance_1.setTransform(-18.4,-27.9,1,1,0,0,0,-6.6,-1.9);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_1},{t:this.instance}]}).to({state:[{t:this.instance_1},{t:this.instance}]},9).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-178.9,-234.6,321.9,395.4);


// stage content:
(lib.BagsLifeHTML_HTML5Canvas = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	this.actionFrames = [0,36,41,46];
	this.streamSoundSymbolsList[36] = [{id:"nasekomyiezvonkiimonotonnyiirezkii",startFrame:36,endFrame:41,loop:1,offset:0}];
	// timeline functions:
	this.frame_0 = function() {
		this.clearAllSoundStreams();
		 
		this.P.addEventListener("click",f1.bind(this));
		function f1(args){this.stop();}
		this.S.addEventListener("click",f2.bind(this));
		function f2(args){this.play();}
		this.G.addEventListener("click",f3.bind(this));
		function f3(args){this.gotoAndStop(0);}
		playSound("jukiskarabei28672",-1);
	}
	this.frame_36 = function() {
		var soundInstance = playSound("nasekomyiezvonkiimonotonnyiirezkii",0);
		this.InsertIntoSoundStreamData(soundInstance,36,41,1);
		playSound("nasekomyieodinochnyiisverchok",-1);
	}
	this.frame_41 = function() {
		playSound("nasekomyieodinochnyiisverchok",-1);
		playSound("nasekomyiezvonkiimonotonnyiirezkii");
	}
	this.frame_46 = function() {
		playSound("nasekomyieodinochnyiisverchok",-1);
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).call(this.frame_0).wait(36).call(this.frame_36).wait(5).call(this.frame_41).wait(5).call(this.frame_46).wait(14));

	// Слой_4
	this.G = new lib.ToStart();
	this.G.name = "G";
	this.G.setTransform(110.25,455.7,0.2691,0.2691);
	new cjs.ButtonHelper(this.G, 0, 1, 2, false, new lib.ToStart(), 3);

	this.S = new lib.StartButton();
	this.S.name = "S";
	this.S.setTransform(74,455.35,0.167,0.167);
	new cjs.ButtonHelper(this.S, 0, 1, 2, false, new lib.StartButton(), 3);

	this.P = new lib.PauseButton();
	this.P.name = "P";
	this.P.setTransform(32.6,452.7,0.552,0.552);
	new cjs.ButtonHelper(this.P, 0, 1, 2, false, new lib.PauseButton(), 3);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.P},{t:this.S},{t:this.G}]}).wait(60));

	// Слой_2
	this.instance = new lib.BagBack();
	this.instance.setTransform(71.4,160.9,0.3483,0.3483,69.6925,0,0,-18.2,-28.1);

	this.instance_1 = new lib.Bag("synched",2);
	this.instance_1.setTransform(285.65,449,0.2627,0.2627,119.9996,0,0,-6.6,-2.4);

	this.Фрагмент_ролика11 = new lib.Bag("synched",0);
	this.Фрагмент_ролика11.name = "Фрагмент_ролика11";
	this.Фрагмент_ролика11.setTransform(167.7,230.05,0.2076,0.2076,-59.997,0,0,-6.7,-1.4);

	this.instance_2 = new lib.Bag("synched",2);
	this.instance_2.setTransform(58.65,257.1,0.2416,0.2416,60.002,0,0,-6.5,-2);

	this.instance_3 = new lib.Bag("synched",6);
	this.instance_3.setTransform(645.55,84.85,0.1772,0.1772,0,-120.0007,59.9993,-6.2,-2.3);

	this.instance_4 = new lib.Bag("synched",4);
	this.instance_4.setTransform(19.55,75.2,0.2027,0.2027,134.9939,0,0,-6.3,-2.1);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_4},{t:this.instance_3},{t:this.instance_2},{t:this.Фрагмент_ролика11},{t:this.instance_1},{t:this.instance}]}).wait(60));

	// Слой_5
	this.instance_5 = new lib.Bag();
	this.instance_5.setTransform(184.95,96.1,0.1313,0.1313,165.0077,0,0,-7,-3.5);

	this.instance_6 = new lib.BagBack();
	this.instance_6.setTransform(485.15,239.65,0.1313,0.1313,-83.8826,0,0,-6.9,-3.5);
	this.instance_6._off = true;

	this.timeline.addTween(cjs.Tween.get(this.instance_5).to({regX:-6.9,rotation:96.1174,guide:{path:[185,96.2,186.4,102.7,188.2,109.1,191.3,120.1,195.3,128.3,200.1,138.2,206.6,145.2,211.5,150.5,221.1,157.3,269.2,191.9,326.2,208.4,329.1,209.3,334.8,210.9,339.8,212.3,343.3,213.6,345.8,214.5,351.1,216.7,356.2,218.9,359,219.8,373.3,224.8,399,225.9,417.5,226.7,419.6,226.9,431.2,227.6,439.8,229.4,445.2,230.5,457.3,234,468.5,237.2,474.8,238.3,478.7,239,485.1,239.9], orient:'fixed'}},36).to({_off:true,rotation:-83.8826},5).to({_off:false,rotation:96.1174},5).to({regX:-6.5,regY:-2.3,rotation:180,guide:{path:[485.1,239.9,489.2,240.4,494.3,240.9,505.7,242.8,512.6,247,517.9,250.2,523.3,256.3,526.4,259.9,532.3,267.5,534.7,270.3,543.9,280.9,551.1,289,555.1,294.5,567.3,311.5,567.5,327,567.5,330.9,566.7,336.4,566.3,339.6,565.3,345.8,564.6,351.3,564.4,358.1,564.3,362.2,564.4,370.5,564.4,397.4,564.5,424.4], orient:'fixed'}},13).wait(1));
	this.timeline.addTween(cjs.Tween.get(this.instance_6).wait(36).to({_off:false},5).to({_off:true,rotation:96.1174},5).wait(14));

	// Слой_1
	this.instance_7 = new lib.Bag();
	this.instance_7.setTransform(136.3,355.45,0.2214,0.2214,90,0,0,-5.9,-2.5);

	this.timeline.addTween(cjs.Tween.get(this.instance_7).to({regX:-5.8,regY:-2.4,scaleX:0.2213,scaleY:0.2213,rotation:10.8196,guide:{path:[136.4,355.5,168.8,355.2,200.3,360,206.2,360.9,217.8,362.8,228.1,364.5,235.3,365.1,240.4,365.5,248.7,365.8,259.3,366.2,261.9,366.4,269.6,366.8,282.2,368.2,297.4,369.9,302.4,370.3,318.8,371.7,338,371.6,353.1,371.6,373.6,370.5,394.3,369.5,408.6,367.9,427.6,365.8,443.2,362,452.6,359.7,459.6,357,468.3,353.7,474.9,349.3,484.8,342.7,492.6,331.9,499.6,322.3,504.5,309.9,508.5,299.5,511.5,285.9,513.5,277.2,516.1,261.3,517.3,254.2,518.1,248.7], orient:'fixed'}},36).to({regX:-5.7,regY:-2.5,rotation:-29.7429,guide:{path:[518.1,248.7,518.9,243.5,519.3,239.8,520.8,227.8,520.9,218.1,521,209,519.2,203.5,518.3,200.8,516.6,197.5,515.6,195.7,513.2,191.8,512,189.9,511,188.2], orient:'fixed'}},5).to({regX:-6.1,regY:-2.2,scaleX:0.2214,scaleY:0.2214,rotation:0,guide:{path:[511,188.2,507.4,182.5,505.1,179.3,500.4,172.6,495.7,167.9,485.3,157.6,468.7,151.1,456.7,146.3,437.4,142.2,412.4,136.9,405.7,135,388.1,130,376.3,122.3,369,117.6,362.8,111.3,357.4,105.7,355.7,100.8,354.6,97.6,354.2,91.1,353.2,75.2,352.2,59.3], orient:'fixed'}},18).wait(1));

	this._renderFirstFrame();

}).prototype = p = new lib.AnMovieClip();
p.nominalBounds = new cjs.Rectangle(295.5,253.6,388.6,243.00000000000003);
// library properties:
lib.properties = {
	id: 'BEACEBABD0788B429063CD96D14129BA',
	width: 640,
	height: 480,
	fps: 20,
	color: "#FFFFFF",
	opacity: 1.00,
	manifest: [
		{src:"sounds/jukiskarabei28672.mp3?1677260925920", id:"jukiskarabei28672"},
		{src:"sounds/nasekomyieodinochnyiisverchok.mp3?1677260925920", id:"nasekomyieodinochnyiisverchok"},
		{src:"sounds/nasekomyiezvonkiimonotonnyiirezkii.mp3?1677260925920", id:"nasekomyiezvonkiimonotonnyiirezkii"}
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
an.compositions['BEACEBABD0788B429063CD96D14129BA'] = {
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