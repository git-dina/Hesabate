   M S B A M L   `   `   `  ����G  CHesabate_POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Hesabate_POS.View.windowsHesabate_POS.View.windows  5   	   5    /Hesabate_POS.View.windows.wd_messageBoxWithIcon   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Hesabate_POS.View.windows     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   -   5          � WindowStyle$
  None=�5	   
    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�5
   	   $��0q�6    � Title$ SC Commerce��5   
   $	��195��6   $	��300��6+   
������  � AllowsTransparency . .��5      � -   5         �Name$ 	grid_main��5   )   $��1��6   $��1��6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��* �6   .+�5   
   � .+�5      �� $��0q�6   $��0��6*    �� CornerRadius$ 7,7,0,0��5      ���#178DD2._�5      � .+�5      �� -   5         ��Name$ 	txt_title��5   /   $��warning��6    
�������$��Center=�5   $   $��Center=�6A   $��1��5      $	��5,0q�64   �� 5      -   5        	 ��Name$	 	btn_colse��5   Y   
 �� IsCancel
 . $��Right=�5   0   $��Center=�6L   
�������$��7.5,0q�5   I   $��SemiBold"�5      $��24��64   $��0q�6?    � ToolTip$ Close��6K   $��14(�6\   ��5   g   Z� ��5      Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  
�������   Kind$ Close=�6E   $��20��5   *   $��20��65   5      5      5       5!      �� 5"   
   $��1,0,1,1q�6   $��1��6,   $ 0,0,7,7��5#      ���#999999
�������._�5$      � -   5        $ grid_mainGrid��5$       5%      �� 5&      $��* �6$   �� 5'      $
��auto �6$    � ColumnDefinitions 5)      �� 5*      $
��auto �6'   �� 5+      $��* �6'   .+�5.      �� $��0��6   ���#737373$
��Bold"�6`   $��14(�6s   $
��Wrap=�5/       �� TextAlignment$ Center=�6*   $��Center=�6B   $��Center=�6^   $��5q�50      $��1��6$   ��5.   ,   �� *contentText1+50   $     51      $��0��6*   
��� ���$ 
InfoCircle=�52      $��10q�6'   $	��100��53      $	��100��6"   �� 54      -   5        $	 btn_ok��55   (   $��1��54      $��Ok��6*   $��2��67   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 7��6K   $��10,7.5q�55      ���#178DD2��56   0   Z� 58      59      5:      5;      