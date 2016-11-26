# Tales of Evil Sword

基于Unity3D的动作角色扮演游戏

</br>



## 目录结构

主要文件都在Assets目录里, 具体文件夹和对应用途如下:

- Scenes 放场景文件(.unity)

- Prefabs 放预设资源, 具体可以看[这里](http://docs.manew.com/Manual/Prefabs.html)

- Scripts 放自己写的脚本代码(如.cs), 不同用途或不同模块的脚本可以单独建立子文件夹存放

- Materials 放材质文件(.mat)

- Models 放3D模型文件(.fbx)

- Animators 放动画控制器(.controller)

- Animations 放动画片段(.fbx)

</br>
创建新文件的时候最好注意一下文件存放的路径(如果在Inpector里直接新建脚本会自动放在Assets根目录, 可以在界面Projects窗口里拖到Scripts的某个文件夹内)

其他文件夹比如Standard Assets, UnityChan都是工程导入的Package, 不用管

尽量不要导入太多的Package, 以免同名同类型的资源的太多容易搞混. 
比如只用到某个Package的一个文件, 只要把文件找出来放进相应目录即可(虽然可能要做些其他改动).

</br>

## 工程使用

在Unity选择工程的界面点Open, 然后选择这个工程文件夹打开,
进去之后不会自动打开某个Scene, 需要在Project(不是菜单那个, 是显示目录树的那个)里找到Scenes文件夹, 双击里面的某个Scene就能看到别人做的场景了.


## 协作方式

很难说一定要按照什么规则来协作, 不过既然大家的分工相互之间还是比较独立的, 我想可以在这个工程上添加自己做的东西, 然后隔几天发我一次, 整合完后我再push上来, 大家再下载下来继续修改添加?


