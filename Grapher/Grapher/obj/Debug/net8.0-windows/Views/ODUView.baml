   M S B A M L   `   `   `  ����B  >Grapher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Grapher.ViewsGrapher.Views  5      [ WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 68       Grapher.Views.ODUView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Grapher.Views     5      hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     68   .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��261* �6   �� 5      $
��139* �6    � RowDefinitions 5   
   �� 5      $	��22* �6   �� 5      $	��53* �6   .+�5   
   ��  �� CornerRadius$ 16��6   $��10q�6#   ��5      �� 5       � ColorInterpolationMode$ ScRgbLinearInterpolation=�6&    � MappingMode$ Absolute=�6X   .��5      �  � Color$ Black��6#    � Offset$	 0.2Q�61   � 5      $ 	#FF1B1B1B��6#   $ 1Q�65   5      ._�5      � .+�5      �� $��10q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6;   $��Center=�6V   .+�5      �� $��Cascadia Code SemiBold)�6$   $��dx/dy = ��6H   
�������$��18(�6k   �� 5      $��Cascadia Code SemiBold)�6"      CustomTextBoxStyle#��[  6h   $��18(�6�   $	��250��6�   ��6F   �� *ExpressionString+6�   5      �� 5      $ 
Horizontal=�6!   $��
10,25,10,0q�6:   .+�5       �� $��Cascadia Code SemiBold)�6$   $��X0 = ��6H   
�������$��16(�6h   �� 5!      $��Cascadia Code SemiBold)�6"   #��[  6F   $��16(�6�   
�������$��50��6�   ��6r   �� *X0+6�   �� 5"      $��Cascadia Code SemiBold)�6$   $��10,0,0,0q�6H   $&�� Конечная точка X1 =��6Z   
�������$��16(�6�   �� 5#      $��Cascadia Code SemiBold)�6"   #��[  6F   $��16(�6�   
�������$��50��6�   ��6r   �� *X1+6�   5$      �� 5%      $ 
Horizontal=�6!   $��
10,10,10,0q�6:   .+�5&      �� $��Cascadia Code SemiBold)�6$   $��Y0 = ��6H   
�������$��16(�6h   �� 5'      $��Cascadia Code SemiBold)�6"   #��[  6F   
�������$��16(�6�   $��50��6�   ��6�   �� *Y0+6�   �� 5(      $��Cascadia Code SemiBold)�6$   $��10,0,0,0q�6H   $'��!Количество шагов =��6Z   
�������$��16(�6�   �� 5)      $��Cascadia Code SemiBold)�6"   #��[  6F   $��16(�6�   
�������$��50��6�   ��6r   �� *Steps+6�   5*      5+      5,      5-      �� 5.   
   $��1��6   $ 16��6!   $��10q�63   ��5/      �� 50      $ ScRgbLinearInterpolation=�6&   $ Absolute=�6X   .��51      � $ Black��6#   $	 0.2Q�61   � 52      $ 	#FF1B1B1B��6#   $ 1Q�65   53      ._�55      � .+�56      �� .+�57      �� $��10q�6    $!��Выберите метод��6,   $��22(�6B    �� TextAlignment$ Center=�6P   
�������$��Cascadia Code SemiBold)�6z   R� 58      .@�59      Q� -   5        $��Метод Эйлера��59   '   6=   Q� 5:      -   5        $(��"Методы Рунге-Кутты��5:   '   6C   Q� 5;      -   5        $��Метод Адамса��5;   '   6=   5<      5=      5>      5?      �� 5@   
   $��1��6   $��2��6   $��10q�62   $ 16��6>   ��5A      �� 5B      $ ScRgbLinearInterpolation=�6&   $ Absolute=�6X   .��5C      � $ Black��6#   $	 0.2Q�61   � 5D      $ 	#FF1B1B1B��6#   $ 1Q�65   5E      ._�5G      �� 
������ $��0q�62   .@�5H      �� $��Cascadia Code SemiBold)�6   $��Практика��6?     CustomTabItem#��[ 6Q   $��16(�6x   .��5I      �� $��Hidden=�6#   .��5J      �� $
��Auto��6%   .+�5K      5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  	 �� BitmapScalingMode$	 HighQuality=�61   
������ $��10q�6y   $��0��6�   
�������
   ZoomMode$

 Both=�6�   $	��250��5L   M   $	��500��6Z      Series 5K   �   �� *Series+   XAxes 5L      �� *XAxes+   YAxes 65   �� *YAxes+5M      �� 5N      $��16(�6(   $��WrapWithOverflow=�66   
�������$��Cascadia Code SemiBold)�6i   $��10q�6�   ��6�   �� *Operation.Description+6�   �� 5O      $��16(�6(   $��WrapWithOverflow=�66   
�������$��Cascadia Code SemiBold)�6i   $��10q�6�   ��6�   �� *Operation.Resolve+6�   5P      5Q      5R      �� 5S      $��Cascadia Code SemiBold)�6   $��Теория��6?   #��[ 6O   $��16(�6v   .��5T      �� .��5U      �� 
�������$��WrapWithOverflow=�6O   ��6$   �� *Theory+6O   5V      5W      5X      5Y      5Z      5[      