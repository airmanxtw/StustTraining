# 將某個時間以前的影像檔resize到800px(800px以上的)
1. 會用到[ImageMagic](https://imagemagick.org/script/index.php),範例:
```
magick mogrify -resize "800>" file1.jpg
``` 
2. 會用到forfiles指令，範例(例出2020/9/30以前的jpg檔)：
```
forfiles /m *.jpg /d -"2020/9/30" /c "cmd /c echo @file"
```
3. forfiles裡的/c指今中，如果有特殊符號，請查ascii hex碼例如0x22代表"
4. [ascii table](http://www.csc.villanova.edu/~tway/resources/ascii-table.html)
5. 將兩個指令結合，完成目標:
```
forfiles /m *.jpg /d -"2022/2/24"  /c "cmd /c magick mogrify -resize 0x22800>0x22 @file"
```