   M S B A M L   `   `   `  ����L  HMaterialDesignApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MaterialDesignAppMaterialDesignApp  5   	   K*clr-namespace:MaterialDesignApp.ViewModelsMaterialDesignApp.ViewModels  5   	   A%clr-namespace:MaterialDesignApp.ViewsMaterialDesignApp.Views  5	   	   "    MaterialDesignApp.UserWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:MaterialDesignApp     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   ;
viewmodels*clr-namespace:MaterialDesignApp.ViewModels  5   	   1views%clr-namespace:MaterialDesignApp.Views  5	   	     � Title$  
UserWindow��5   	   $	��940��6   $
��1480��6)   &   System.Windows.Media.TextOptions   TextRenderingMode$ Aliased=�5   	   $��18(�61   $9��3/MaterialDesignApp;component/Montserrat/#Montserrat)�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������  � 
ResizeMode$ NoResize=�5   "    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ /Images/joystick.png��5   	    � 	Resources 5       �� DataType8  2MaterialDesignApp.ViewModels.MyCharactersViewModel.   (MaterialDesignApp.Views.MyCharactersView	 � DataContext0  *MaterialDesignApp.ViewModels.GameViewModel&    MaterialDesignApp.Views.GameView0  *MaterialDesignApp.ViewModels.ShopViewModel&    MaterialDesignApp.Views.ShopView0  *MaterialDesignApp.ViewModels.NewsViewModel&	    MaterialDesignApp.Views.NewsView4
  .MaterialDesignApp.ViewModels.DownloadViewModel*   $MaterialDesignApp.Views.DownloadView4  .MaterialDesignApp.ViewModels.SettingsViewModel*   $MaterialDesignApp.Views.SettingsView3  -MaterialDesignApp.ViewModels.AccountViewModel)   #MaterialDesignApp.Views.AccountView��%  (��       5   9   *, +)(�� ]     5   1   *, +)(�� �     5   1   *, +)(��     5   1   *, +)(�� \    5   5   *,
 +)(�� �    5"   5   *, +)(��     5%   4   *, +)�� 5   
   MyCharactersViewTemplate��"  69   .R�5        	 6%   �� 5      �� 5   
   GameViewTemplate��"  61   .R�5        	 6   �� 5      �� 5   
   ShopViewTemplate��"  61   .R�5        	 6   �� 5      �� 5   
   NewsViewTemplate��"  61   .R�5      	  	 6   �� 5      �� 5   
   DownloadViewTemplate��" 
 65   .R�5         	 6!   �� 5!      �� 5"   
   SettingsViewTemplate��"  65   .R�5#        	 6!   �� 5$      �� 5%   
   AccountViewTemplate��"  64   .R�5&        	 6    �� 5'      5(      .��5)      � .+�5*   
   �� 
������ 
 �� Padding$
 20q�6*    J� Effect 5+      3  -System.Windows.Media.Effects.DropShadowEffect  5,         
BlurRadius$ 30Q�6#      	Direction$ 0Q�63      ShadowDepth$ 1Q�6A      Opacity$	 0.2Q�6Q   ._�5.      �� -   5         ��Name$ Wrapper��5.      ���#FFF �� CornerRadius$ 0��68   ._�5/      �� .+�50      � -   5         �Name$ Navbar��50      $	��Top=�6+   $��85��6C   ��51      �� 52       �� EndPoint$
 1, 1&�62    �� 
StartPoint$
 0, 0&�6B   .��53   "   �  � Color$ #681565��6/    � Offset$ 0Q�6?   � 54   "   $ #673AB7��6/   $ 1Q�6?   55       � ColumnDefinitions 57      �� 58      $��0.0001* �6/   �� 59      $
��0.3* �6/   �� 5:      $��0.18* �6/   �� 5;      $��0.18* �6/   �� 5<      $��0.03* �6/   �� 5=      $��0.21* �6/   .+�5?      �� $��0��6!   }�. ._�5@      �� 
������ 
���   �$��
0, 0, 0, 0q�5A   %   $��
0, 0, 0, 0q�6B    5B   "     5C   &   $ 0Q�67   $ 30Q�6G   5E      5F      �� 5G      -   5        $��1��5G   !    � Cursor$
 Hand��61   }�. ._�5I      �� -   5        
������ 
���   �$��
0, 0, 0, 0q�5J   %   $��
0, 0, 0, 0q�6B    5N   "     5O   &   $ 0Q�67   $ 30Q�6G   ._�5Q   "   �� -   5         ��Name$ MyCharactersButton��5Q   (   $��Мои персонажи��6D   $��22(�5R   !   $��SemiBold"�6/   
�������$��Center=�5S   !   $��Center=�6>   5T      5U      �� 5V      -   5        $��2��5V   !   $
 Hand��61   }�. ._�5X      �� -   5        
������ 
���   �$��
0, 0, 0, 0q�5Y   %   $��
0, 0, 0, 0q�6B    5]   "     5^   &   $ 0Q�67   $ 30Q�6G   ._�5`   "   �� -   5        $ 
GameButton��5`   (   $��Игра��6<   $��22(�5a   !   $��SemiBold"�6/   
�������$��Center=�5b   !   $��Center=�6>   5c      5d      �� 5e      -	   5        $��3��5e   !   $
 Hand��61   }�. ._�5g      �� -
   5        $ ShopWrapper��5g   %   
������ 
���   �$��
0, 0, 0, 0q�5i   %   $��
0, 0, 0, 0q�6B    5m   "     5n   &   $ 0Q�67   $ 30Q�6G   ._�5p   "   �� -   5        $ 
ShopButton��5p   (   $��Магазин��6<   $��22(�5q   !   $��SemiBold"�6/   
�������$��Center=�5r   !   $��Center=�6>   5s      5t      �� 5u      $��4��6!   }�. ._�5v      �� 
������ 
���   �$��
0, 0, 0, 0q�5w   %   $��
0, 0, 0, 0q�6B    5x   "     5y   &   $ 0Q�67   $ 30Q�6G   5{      5|      � 5}      $��5��6   .+�5~      � $	��130��6#   $
��Left=�6/   .+�5   "   �� -   5         ��Name$ SettingsButton��5   (   $��/Images/settings.png��5�   (   $��50��6F   $
��Left=�5�   (   $
 Hand��5�   (    �� BitmapScalingMode$ HighQuality=�66    5�   &     5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   �� 5�   "   -   5        $ AccountButton��5�   (   $��/Images/account.png��5�   (   $��50��6E   $��Right=�5�   (   $
 Hand��5�   (   $ HighQuality=�66    5�   &     5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   5�      � 5�      $	��130��6#   $��Right=�6/   $��0 0 20 0q�6K   .+�5�   "   �� -   5        $ MinimizeButton��5�   (   $��/Images/minimize.png��5�   (   $��50��6F   $
��Left=�5�   (   $
 Hand��5�   (   $ HighQuality=�66    5�   &     5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   �� 5�   "   -   5        $ CloseButton��5�   (   $��/Images/close_white.png��5�   (   $��50��6I   $��Right=�5�   (   $
 Hand��5�   (   $ HighQuality=�66    5�   &     5�   *   $	 -90Q�6;   $ 5Q�6K   $ 5Q�6Z   $	 0.2Q�6j   5�   #   5�      5�      5�      �� 5�      -   5         ��Name$ Content��5�   %   ��66   �� 5�      5�      5�      5�      5�      