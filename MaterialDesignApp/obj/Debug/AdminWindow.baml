   M S B A M L   `   `   `  ����L  HMaterialDesignApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MaterialDesignAppMaterialDesignApp  5   	   K*clr-namespace:MaterialDesignApp.ViewModelsMaterialDesignApp.ViewModels  5   	   A%clr-namespace:MaterialDesignApp.ViewsMaterialDesignApp.Views  5	   	   #    MaterialDesignApp.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:MaterialDesignApp     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   ;
viewmodels*clr-namespace:MaterialDesignApp.ViewModels  5   	   1views%clr-namespace:MaterialDesignApp.Views  5	   	     � Title$  
UserWindow��5   	   $	��940��6   $
��1480��6)   &   System.Windows.Media.TextOptions   TextRenderingMode$ Aliased=�5   	   $��18(�61   $9��3/MaterialDesignApp;component/Montserrat/#Montserrat)�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������  � 
ResizeMode$ NoResize=�5   "    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ /Images/joystick.png��5   	    � 	Resources 5       �� DataType3  -MaterialDesignApp.ViewModels.PlayersViewModel)   #MaterialDesignApp.Views.PlayersView	 � DataContext3  -MaterialDesignApp.ViewModels.ReportsViewModel)   #MaterialDesignApp.Views.ReportsView4  .MaterialDesignApp.ViewModels.SettingsViewModel*   $MaterialDesignApp.Views.SettingsView3  -MaterialDesignApp.ViewModels.AccountViewModel)	   #MaterialDesignApp.Views.AccountView��%�  (��       5   4   *, +)(�� X     5   4   *, +)(�� �     5   5   *, +)(�� 	    5   4   *, +)�� 5   
   PlayersViewTemplate��"  64   .R�5        	 6    �� 5      �� 5   
   ReportsViewTemplate��"  64   .R�5        	 6    �� 5      �� 5   
   SettingsViewTemplate��"  65   .R�5        	 6!   �� 5      �� 5   
   AccountViewTemplate��"  64   .R�5      	  	 6    �� 5      5      .��5       � .+�5!   
   �� 
������ 
 �� Padding$
 20q�6*    J� Effect 5"      3
  -System.Windows.Media.Effects.DropShadowEffect
  5#       
  
BlurRadius$ 30Q�6#    
  	Direction$ 0Q�63    
  ShadowDepth$ 1Q�6A    
  Opacity$	 0.2Q�6Q   ._�5%      �� -   5         ��Name$ Wrapper��5%      ���#FFF �� CornerRadius$ 0��68   ._�5&      �� .+�5'      � -   5         �Name$ Navbar��5'      $	��Top=�6+   $��85��6C   ��5(      �� 5)       �� EndPoint$
 1, 1&�62    �� 
StartPoint$
 0, 0&�6B   .��5*   "   �  � Color$ #681565��6/    � Offset$ 0Q�6?   � 5+   "   $ #673AB7��6/   $ 1Q�6?   5,       � ColumnDefinitions 5.      �� 5/      $��0.0001* �6/   �� 50      $��0.33* �6/   �� 51      $��0.33* �6/   �� 52      $��0.03* �6/   �� 53      $��0.21* �6/   .+�55      �� $��0��6!   }�. ._�56      �� 
������ 
���   �$��
0, 0, 0, 0q�57   %   $��
0, 0, 0, 0q�6B    58   "   
  59   &   $ 0Q�67   $ 30Q�6G   5;      5<      �� 5=      -   5        $��1��5=   !    � Cursor$
 Hand��61   }�. ._�5?      �� -   5        
������ 
���   �$��
0, 0, 0, 0q�5@   %   $��
0, 0, 0, 0q�6B    5D   "   
  5E   &   $ 0Q�67   $ 30Q�6G   ._�5G   "   �� -   5         ��Name$ PlayersButton��5G   (   $��Игроки��6?   $��22(�5H   !   $��SemiBold"�6/   
�������$��Center=�5I   !   $��Center=�6>   5J      5L      �� 5M      -   5        $��2��5M   !   $
 Hand��61   }�. ._�5O      �� -   5        
������ 
���   �$��
0, 0, 0, 0q�5P   %   $��
0, 0, 0, 0q�6B    5T   "   
  5U   &   $ 0Q�67   $ 30Q�6G   ._�5W   "   �� -   5        $ ReportsButton��5W   (   $��Отчеты��6?   $��22(�5X   !   $��SemiBold"�6/   
�������$��Center=�5Y   !   $��Center=�6>   5Z      5\      �� 5]      $��3��6!   }�. ._�5^      �� 
������ 
���   �$��
0, 0, 0, 0q�5_   %   $��
0, 0, 0, 0q�6B    5`   "   
  5a   &   $ 0Q�67   $ 30Q�6G   5c      5d      � 5e      $��5��6   .+�5f      � $	��130��6#   $
��Left=�6/   .+�5g   "   �� -	   5         ��Name$ SettingsButton��5g   (   $��/Images/settings.png��5h   (   $��50��6F   $
��Left=�5i   (   $
 Hand��5j   (    �� BitmapScalingMode$ HighQuality=�66    5m   &   
  5n   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5p   #   �� 5q   "   -
   5        $ AccountButton��5q   (   $��/Images/account.png��5r   (   $��50��6E   $��Right=�5s   (   $
 Hand��5t   (   $ HighQuality=�66    5w   &   
  5x   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5z   #   5{      � 5|      $	��130��6#   $��Right=�6/   $��0 0 20 0q�6K   .+�5}   "   �� -   5        $ MinimizeButton��5}   (   $��/Images/minimize.png��5~   (   $��50��6F   $
��Left=�5   (   $
 Hand��5�   (   $ HighQuality=�66    5�   &   
  5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   �� 5�   "   -   5        $ CloseButton��5�   (   $��/Images/close_white.png��5�   (   $��50��6I   $��Right=�5�   (   $
 Hand��5�   (   $ HighQuality=�66    5�   &   
  5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   5�      5�      5�      �� 5�      -   5         ��Name$ Content��5�   %   ��66   �� 5�      5�      5�      5�      5�      