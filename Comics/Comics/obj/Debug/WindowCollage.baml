   M S B A M L   `   `   `  ����A  =Comics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ComicsComics  5   	       Comics.WindowCollage   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Comics     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  WindowCollage��5	   	   $
��1100��6   $	��600��6/   $
��1100��6?   $	��600��6O   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��59* �6   �� 5      $
��150* �6   �� 5      $	��64* �6    � RowDefinitions 5   
   �� 5      $
��429* �6   �� 5      $
��140* �6   .+�5   
   � $��1��6   $��1��6#   $��1��63   $��1��6D   ���#ffab91.+�5      �� -   5         ��Name$ scrollViewerCollages��5      $��Visible=�69   $��Disabled=�6a   $��5,5,5,5q�5      .��5      �� -   5         ��Name$ stackPanelCollages��5      $��3,3,3,3q�69    �� Orientation$ 
Horizontal=�6J   5      5      � 5   
   $��1��6   $��2��6#   $��1��63   $��0��6D   .+�5      �� -   5        $ scrollViewerImages��5      $��Disabled=�67   $��Visible=�6`   $��5,5,5,5q�5      .��5      �� -   5        $ stackPanelImages��5      $��3,3,3,3q�67   $ Vertical=�6H   5      5      � 5    
   $��0��6   .+�5!      �� $��/Resources/1.png��6   $��70��6.   $��60��69   $��156,83,10,286q�6E    J� Effect 5"      3  -System.Windows.Media.Effects.DropShadowEffect  5#         Opacity$	 0.5Q�6'   67   5%      �� 5&      -   5         ��Name$ btnV1��5&      $
��Left=�6$   	 � ToolTip$	 Добавить��6?   $��171,148,0,0q�6R    *  &MaterialDesignFloatingActionMiniButton#��[  6g   $	��Top=�5'      $��40��6-   $��40��69   .��5(      '  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 FiberManualRecord=�6*   $��24��6C   $��24��6O   5)      �� 5*      $��/Resources/2.png��6   $��70��6.   $��60��69   $��10,83,156,286q�6E    5+        5,      $	 0.5Q�6'   67   5.      �� 5/      -   5        $ btnV2��5/      $
��Left=�6$   $	 Добавить��6?   $��
25,148,0,0q�6R   #��[  6f   $	��Top=�50      $��40��6-   $��40��69   .��51        $
 FiberManualRecord=�6*   $��24��6C   $��24��6O   52      �� 53      $��/Resources/3.png��6   $��70��6.   $��60��69   $��156,244,10,125q�6E    54        55      $	 0.5Q�6'   67   57      �� 58      -   5        $ btnV3��58      $
��Left=�6$   $	 Добавить��6?   #��[  6R   $	��Top=�59      $��40��6-   $��40��69   $��171,309,5,5q�6X   .��5:        $
 FiberManualRecord=�6*   $��24��6C   $��24��6O   5;      �� 5<      $��Resources/4.png��6   $��70��6-   $��60��68   $��10,244,156,125q�6D    5=        5>      $	 0.5Q�6'   67   5@      �� 5A      -	   5        $ btnV4��5A      $
��Left=�6$   $	 Добавить��6?   $��
28,309,0,0q�6R   #��[  6f   $	��Top=�5B      $��40��6-   $��40��69   .��5C        $
 FiberManualRecord=�6*   $��24��6C   $��24��6O   5D      �� 5E      -
   5         ��Name$ label��5E      $$��Добавить коллаж:��6#   $��Center=�6>   $��10,10q�6[   $��17(�6j   $	��Top=�6x   $	��163��6�   $��35��6�   5F      � 5G   
   $��1��6   $��1��6#   $��1��63   $��0��6D   .+�5H      �� 
���   �$��1q�6)   $	��410��6>   $	��560��6K   $��Center=�5I      $��Center=�60   ._�5J      � -   5         �Name$ 
gridEditor��5J      5K      5L      5M      � 5N   
   $��1��6   $��2��6#   $��1��63   $��2��6D   ���#ffab91.+�5O      �� -   5        $ btnSave��5O      $	 Сохранить��6&   $��Center=�6:    & "MaterialDesignFloatingActionButton#��[ 6W   $��Center=�5P      $��40��6B   $��40��6N   .��5Q        $
 ContentSave=�6*   $��24��6=   $��24��6I   5R      5S      � 5T   
   $��1��6   $��0��6#   $��1��63   $��2��6D   ���#ffab91.+�5U      �� -   5        $ btnBack��5U      $	 
Назад��6&   $��Center=�66   #��[ 6S   $��Center=�5V      $��40��6-   $��40��68   .��5W        $
 reply=�6*   $��24��67   $��24��6C   5X      5Y      5Z      5[      