   M S B A M L   `   `   `  ����B  >Grapher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GrapherGrapher  5   	   7 clr-namespace:Grapher.ViewModelsGrapher.ViewModels  5   	   C&clr-namespace:Grapher.ViewModels.ViewsGrapher.ViewModels.Views  5   	   -clr-namespace:Grapher.ViewsGrapher.Views  5
   	       Grapher.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Grapher     5   	   /vm clr-namespace:Grapher.ViewModels     5   	   6vmv&clr-namespace:Grapher.ViewModels.Views     5   	   *vvclr-namespace:Grapher.Views     5
   	   -   5        $��
mainWindow��5   	     � Title$  Grapher��5   	   $	��720��6   $
��1080��6&    � AllowsTransparency$
 True��63    � WindowStyle$
 None=�6c    � WindowStartupLocation$ CenterScreen=�6v   ��6M   Z�  � 	Resources 5       �� DataType,   &Grapher.ViewModels.Views.InfoViewModel   Grapher.Views.InfoView � DataContext-   'Grapher.ViewModels.Views.GraphViewModel   Grapher.Views.GraphView1   +Grapher.ViewModels.Views.CalculateViewModel!   Grapher.Views.CalculateView,   &Grapher.ViewModels.Views.SLAUViewModel   Grapher.Views.SLAUView+	   %Grapher.ViewModels.Views.ODUViewModel
   Grapher.Views.ODUView��%�  (��       5      *, +)(�� @     5      *, +)(�� �     5      *, +)(�� �     5      *, +)(��      5      *,	 +)�� 5   
   "  6   .R�5         6   �� 5      �� 5   
   "  6   .R�5         6   �� 5      �� 5   
   "  6   .R�5         6   �� 5      �� 5   
   "  6   .R�5         6   �� 5      �� 5   
   " 	 6   .R�5      
   6   �� 5      5      .��5      ��  �� CornerRadius$ 12��6   ��5    
   �� 5!       � ColorInterpolationMode$ ScRgbLinearInterpolation=�6"   	 � MappingMode$	 Absolute=�6T   .��5"      � 
 � Color$
 Black��6    � Offset$	 0.2Q�6-   � 5#      $
 	#FF131313��6   $ 1Q�61   5$      ._�5&   
   �  � ColumnDefinitions 5'      �� 5(      $	��71* �6#   �� 5)      $
��289* �6#    � RowDefinitions 5+      �� 5,      $��2* �6    �� 5-      $	��43* �6    .+�5/      �� -   5        $��UpBar��5/      $ 8��6"   $��2��63   ��50      �� 51      $ ScRgbLinearInterpolation=�6*   $	 Absolute=�6\   .��52      � $
 Black��6'   $	 0.2Q�65   � 53      $
 	#FF131313��6'   $ 1Q�69   54      ._�56      � .+�57      ��  �� Orientation$ 
Horizontal=�6!   $��0��6:   $��1��6G   $��Right=�6W   $	��120��6s   .+�58      ��    MinimizeButton#���   6!   $	��—��6J   
�������$��60��6i   $��24(�6t   $��Cascadia Code SemiBold)�6�   ��59      �� *MinimizeCommand+��6=   %  System.Windows.Markup.Reference     Name$ 
mainWindow���� 5:        
ExitButton#���  6!   $��X��6F   
�������$��60��6e   $��22(�6p   $��Cascadia Code SemiBold)�6~   ��5;      �� *ExitCommand+��69     $ 
mainWindow��5<      5=      5>      �� 5?      $��0��6   $��2��6&   .+�5@      �� $��/Assets/grapher.png��6    �� BitmapScalingMode$ HighQuality=�65   $	��125��6c   $��	0,25,0,10q�6o   �� 5A      $��Grapher��6   $��36(�6+    �� TextAlignment$ Center=�69   
�������$��Cascadia Code SemiBold)�6c   �� 5B      ���	#FF7B7B7B$	��1.2��6-   $��	0,10,0,25q�6:   �� 5C        MenuButtonStyle#���  6   $��50��5D   ?   $	��200��6K   $��Graph��6W   ��6   �� *SwitchMenuCommand+.��5E      �� $ 
Horizontal=�6!   $
��Left=�6:   $	��175��6U   .+�5F      �� $��/Assets/growth.png��6    $��35��6<   $ HighQuality=�6G   �� 5G      $��График��6$   $
��10,0q�62   
�������$ Center=�6S   $��Center=�6j   $��Arial Rounded MT Bold)�6�   $��24(�6�   5H      5I      �� 5J      #���  6   $��50��6C   $	��200��6O   $��	Calculate��5K   ?   ��6   �� *SwitchMenuCommand+.��5L      �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5M      �� $��/Assets/calculator.png��6    $��35��6@   $ HighQuality=�6K   �� 5N      $'��!Решение уравнений��6$   $
��10,0q�6=   $	��120��6K   $��WrapWithOverflow=�6W   
�������$ Center=�6�   $��Center=�6�   $��Arial Rounded MT Bold)�6�   $��20(�6�   5O      5P      �� 5Q      #���  6   $��50��6C   $	��200��6O   $
��SLAU��5R   ?   ��6   �� *SwitchMenuCommand+.��5S      �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5T      �� $��/Assets/matrix.png��6    $��35��6<   $ HighQuality=�6G   �� 5U      $��СЛАУ��6$   $
��10,0q�60   $	��120��6>   $��WrapWithOverflow=�6J   
�������$ Center=�6}   $��Center=�6�   $��Arial Rounded MT Bold)�6�   $��20(�6�   5V      5W      �� 5X      #���  6   $��50��6C   $	��200��6O   $	��ODU��5Y   ?   ��6   �� *SwitchMenuCommand+.��5Z      �� $ 
Horizontal=�6!   $
��Left=�6:   .+�5[      �� $��/Assets/function.png��6    $��35��6>   $ HighQuality=�6I   �� 5\      $��ОДУ��6$   $
��10,0q�6/   $	��120��6=   $��WrapWithOverflow=�6I   
�������$ Center=�6|   $��Center=�6�   $��Arial Rounded MT Bold)�6�   $��20(�6�   5]      5^      5`      �� 5a      #���  6   $	��Top=�6?   $��1��6W   $��50��6d   $��	0,591,0,0q�6p   $	��FAQ��5b   ;   ��6   �� *SwitchMenuCommand+.��5c      �� $ 
Horizontal=�6   .+�5d      �� $��/Assets/help.png��6   $��35��66   $ HighQuality=�6A   �� 5e      $	��FAQ��6    $
��10,0q�6+   
�������$ Center=�6L   $��Center=�6c   $��Arial Rounded MT Bold)�6~   $��24(�6�   5f      5g      �� 5h      $��1��6   $��5,0,0,0q�6P   $��1��6a   ��6*   �� *SelectedViewModel+6a   �� 5i      $��2��6   $
��Left=�6*   $��1��6E   ���	#FF7B7B7B$��0,0,0,0q�6`   $��1��6q   5j      5k      5l      