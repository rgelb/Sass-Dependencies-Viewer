# Sass Dependencies Viewer
Shows a tree structure of SASS dependencies as defined by `@import` statements.

At the moment the app only support `@import` statements with explicit or relative file name declarations:

```
@import 'header/top.scss'
@import 'c:\header\top.scss'
@import 'tables.scss'
```


The application does not support statements with multiple declarations, urls or folder: 

```
@import 'header.scss', 'sidebar.scss', 'footer.scss'
@import 'MyStylesFolder'
@import 'url(www.foo.com/site.scss)'
```

![Screenshot](/misc/sassfileviewer.png)
