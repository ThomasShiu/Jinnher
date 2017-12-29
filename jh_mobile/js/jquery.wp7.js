$(function () {
    // 先取得 div#abgne_marquee ul
    // 接著把 ul 中的 li 項目再重覆加入 ul 中(等於有兩組內容)
    // 再來取得 div#abgne_marquee 的高來決定每次跑馬燈移動的距離
    // 設定跑馬燈移動的速度及輪播的速度
    var $marqueeUl = $('div.run ul'),
			$marqueeli = $marqueeUl.append($marqueeUl.html()).children(),
			_height = $('div.run').height() * -1,
			scrollSpeed = 1000,
			timer,
			speed = 6000 + scrollSpeed;

    var $marqueeUl2 = $('div.run2 ul'),
			$marqueeli2 = $marqueeUl2.append($marqueeUl2.html()).children(),
			_height = $('div.run2').height() * -1,
			scrollSpeed2 = 900,
			timer2,
			speed2 = 5000 + scrollSpeed2;

    var $marqueeUl3 = $('div.run3 ul'),
			$marqueeli3 = $marqueeUl3.append($marqueeUl3.html()).children(),
			_height = $('div.run3').height() * -1,
			scrollSpeed3 = 800,
			timer3,
			speed3 = 9000 + scrollSpeed3;

    var $marqueeUl4 = $('div.run4 ul'),
			$marqueeli4 = $marqueeUl4.append($marqueeUl4.html()).children(),
			_height = $('div.run4').height() * -1,
			scrollSpeed4 = 700,
			timer4,
			speed4 = 8000 + scrollSpeed4;

    var $marqueeUl5 = $('div.run5 ul'),
		$marqueeli5 = $marqueeUl5.append($marqueeUl5.html()).children(),
		_height = $('div.run5').height() * -1,
		scrollSpeed5 = 600,
		timer5,
		speed5 = 3000 + scrollSpeed5;
    $marqueeUl5.css('top', $marqueeli5.length / 2 * _height);


    // 幫左邊 $marqueeli 加上 hover 事件
    // 當滑鼠移入時停止計時器；反之則啟動
    $marqueeli.hover(function () {
        clearTimeout(timer);
    }, function () {
        timer = setTimeout(showad, speed);
    });

    $marqueeli2.hover(function () {
        clearTimeout(timer2);
    }, function () {
        timer2 = setTimeout(showad2, speed2);
    });

    $marqueeli3.hover(function () {
        clearTimeout(timer3);
    }, function () {
        timer3 = setTimeout(showad3, speed3);
    });

    $marqueeli4.hover(function () {
        clearTimeout(timer4);
    }, function () {
        timer4 = setTimeout(showad4, speed4);
    });

    $marqueeli5.hover(function () {
        clearTimeout(timer5);
    }, function () {
        timer5 = setTimeout(showad5, speed5);
    });

    // 控制跑馬燈移動的處理函式
    function showad() {
        var _now = $marqueeUl.position().top / _height;
        _now = (_now + 1) % $marqueeli.length;

        // $marqueeUl 移動
        $marqueeUl.animate({
            top: _now * _height
        }, scrollSpeed, function () {
            // 如果已經移動到第二組時...則馬上把 top 設 0 來回到第一組
            // 藉此產生不間斷的輪播
            if (_now == $marqueeli.length / 2) {
                $marqueeUl.css('top', 0);
            }
        });

        // 再啟動計時器
        timer = setTimeout(showad, speed);
    }

    function showad2() {
        var _now = $marqueeUl2.position().top / _height;
        _now = (_now + 1) % $marqueeli2.length;

        // $marqueeUl 移動
        $marqueeUl2.animate({
            top: _now * _height
        }, scrollSpeed2, function () {
            // 如果已經移動到第二組時...則馬上把 top 設 0 來回到第一組
            // 藉此產生不間斷的輪播
            if (_now == $marqueeli2.length / 2) {
                $marqueeUl2.css('top', 0);
            }
        });

        // 再啟動計時器
        timer2 = setTimeout(showad2, speed2);
    }

    function showad3() {
        var _now = $marqueeUl3.position().top / _height;
        _now = (_now + 1) % $marqueeli3.length;

        // $marqueeUl 移動
        $marqueeUl3.animate({
            top: _now * _height
        }, scrollSpeed3, function () {
            // 如果已經移動到第二組時...則馬上把 top 設 0 來回到第一組
            // 藉此產生不間斷的輪播
            if (_now == $marqueeli3.length / 2) {
                $marqueeUl3.css('top', 0);
            }
        });

        // 再啟動計時器
        timer3 = setTimeout(showad3, speed3);
    }

    function showad4() {
        var _now = $marqueeUl4.position().top / _height;
        _now = (_now + 1) % $marqueeli4.length;

        // $marqueeUl 移動
        $marqueeUl4.animate({
            top: _now * _height
        }, scrollSpeed4, function () {
            // 如果已經移動到第二組時...則馬上把 top 設 0 來回到第一組
            // 藉此產生不間斷的輪播
            if (_now == $marqueeli4.length / 2) {
                $marqueeUl4.css('top', 0);
            }
        });

        // 再啟動計時器
        timer4 = setTimeout(showad4, speed4);
    }


    function showad5() {
        var _now = $marqueeUl5.position().top / _height;
        _now = (_now - 1 + $marqueeli5.length) % $marqueeli5.length;

        // $marqueeUl 移動
        $marqueeUl5.animate({
            top: _now * _height
        }, scrollSpeed5, function () {
            // 如果已經移動到第一組時...則馬上把 top 設為顯示到第二組內容的第一筆
            // 藉此產生不間斷的輪播
            if (_now == 0) {
                $marqueeUl5.css('top', $marqueeli5.length / 2 * _height);
            }
        });

        // 再啟動計時器
        timer5 = setTimeout(showad5, speed5);
    }

    // 啟動計時器
    timer = setTimeout(showad, speed);

    timer2 = setTimeout(showad2, speed2);

    timer3 = setTimeout(showad3, speed3);

    timer4 = setTimeout(showad4, speed4);

    timer5 = setTimeout(showad5, speed5);

    $('a').focus(function () {
        this.blur();
    });
});