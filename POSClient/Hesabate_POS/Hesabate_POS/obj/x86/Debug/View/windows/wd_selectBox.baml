   M S B A M L   `   `   `  ����G  CHesabate_POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Hesabate_POS.View.windowsHesabate_POS.View.windows  5   	   ,    &Hesabate_POS.View.windows.wd_selectBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Hesabate_POS.View.windows     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   -   5          � WindowStyle$
  None=�5	   
    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�5
   	   $��0q�6   $	��155��5   
   $	��300��6   
������  � AllowsTransparency . .��5      � -   5         �Name$ 	grid_main��5   +   $��1��6   $��1��6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   � .+�5      �� $��0q�6   $��0��6*    �� CornerRadius$ 7,7,0,0��5         	MainColor#��[  6-   ._�5      � .+�5      �� -   5         ��Name$ 	btn_colse��5   Y    �� IsCancel . $��Right=�5   0   $��Center=�6L   
�������$��7.5,0q�5   I   $��SemiBold"�5      $��24��64   $��0q�6?   	 � ToolTip$	 Close��6K   $��14(�6\   ��5   g   Z� ��5      Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  
�������
   Kind$
 Close=�6E   $��20��5   *   $��20��65   5      � 5      $��Center=�6    � ColumnDefinitions 5      �� 5      $��* �6/   �� 5       $
��1.2* �6/   .+�5&      �� -   5         ��Name$ 	txt_title��5&   5   $��	selectBox��6$   
�������$
��Left=�5'   $   $��Center=�6?   $��1��5(      $	��5,0q�64   5)      5*      5+      5,      �� 5-   
   $��1,0,1,1q�6   $��1��6,   $ 0,0,7,7��5.      ���#999999
�������._�5/      � $��5q�6    50      �� 51      $
��auto �6$   �� 52      $��* �6$   �� 53      $
��auto �6$   .+�56      �� $ 0��6   $��10,5,5,5q�6+   $��0 0 0 1q�6?     Grey#��[ 6Y   ._�57      � .+�58      �� -   5         ��Name$ cb_selectBox��58   #   $��10,0,5,0q�59   #     	textColor#��[ 67   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ selectBox...��5:   #    & "MaterialDesignFloatingHintComboBox#��[ 5;   $   $��0��5<   #   $��40��63   $	��0,0q�6@   ���#737373$��0q�5=   9   F� 5>      -   5         F�Name$ p_error_selectBox��5>   .   ���#E81123 �� Stretch$
 Fill=�6I   $��5,5,20,5q�6X   $��Right=�5?      $��8��6;   $y�	Collapsed=�6I   $��Center=�5@      $��15��65   $��15��6A     
infoCircle#��[ 5A      5B      5C      �� 5E      -   5        $ btn_save��5F   &   $��6��5E      $��2��6(   $
��save��6>   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 7��6M   $
��5,10q�5F      
�������#��[  5G      ��6C   Z� 5H      5J      5M      5N      