   M S B A M L   `   `   `  ����B  >Grapher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Grapher.ViewsGrapher.Views  5      C&clr-namespace:Grapher.ViewModels.ViewsGrapher.ViewModels.Views  5      [ WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5      !    Grapher.Views.CalculateView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Grapher.Views     5      6vmv&clr-namespace:Grapher.ViewModels.Views     5      hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5      .��5      �   � ColumnDefinitions  5   
   �� 5      $
��163* �6   �� 5      $
��140* �6    � RowDefinitions 5   
   �� 5      $
��273* �6   �� 5      $
��400* �6   .+�5   
   ��  �� Padding$ 10q�6    �� CornerRadius$ 8��6   $��1��6/   $��10,10,10,10q�6@   $��1��6U   ��5      �� 5       � ColorInterpolationMode$ ScRgbLinearInterpolation=�6&    � MappingMode$ Absolute=�6X   .��5      �  � Color$ Black��6#    � Offset$	 0.2Q�61   � 5      $ 	#FF131313��6#   $ 1Q�65   5      ._�5      � .+�5      5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  -   5        
������ $��0��5   �   
�������   ZoomMode$
 Both=�6�   ,  &LiveChartsCore.SkiaSharpView.WPF.Chart	   DataPointerDownCommand	 6{   �� *
AddPoint+
   Series
 6�   �� *Series+   XAxes 5      �� *XAxes+   YAxes 65   �� *YAxes+5      �� 5       $	��Top=�6    �� Orientation$ 
Horizontal=�65   $��Right=�6N   $	��320��6j   .+�5!      �� $��16(�6    
������� �� TextAlignment$ Right=�6A   $��Точки ��6W   $��0,0,0,5q�6e   $��Cascadia Code SemiBold)�6v   �� 5"      $ 
Horizontal=�6!   .+�5#      �� $��16(�6$   
�������$ Right=�6E   $
��  A:��6[   $��Cascadia Code SemiBold)�6g   �� 5$      $��16(�6$   
�������$
 Left=�6E   $��75��6Z   $��Cascadia Code SemiBold)�6�   ��6e   �� *A+ �� StringFormat$ {0:F2}��6�   5%      �� 5&      $ 
Horizontal=�6!   .+�5'      �� $��16(�6$   
�������$ Right=�6E   $��B:��6[   $��Cascadia Code SemiBold)�6e   �� 5(      $��16(�6$   
�������$
 Left=�6E   $��75��6Z   $��Cascadia Code SemiBold)�6�   ��6e   �� *B+$ {0:F2}��6�   5)      5*      5+      5,      �� 5-   
   $ 5q�6   $ 8��6   $��1��6.   $��10,10,10,10q�6?   $��1��6T   ��5.      �� 5/      $ ScRgbLinearInterpolation=�6&   $ Absolute=�6X   .��50      � $ Black��6#   $	 0.2Q�61   � 51      $ 	#FF1B1B1B��6#   $ 1Q�65   52      ._�54      � .+�55      R� .@�56      Q� $'��!Решение уравнений��6#   .@�57      Q� -   5        $6��0Метод половинного деления��57   '   6J   Q� 58      -   5        $��Метод Ньютона��58   '   6>   Q� 59      -   5        $��Метод секущих��59   '   6>   Q� 5:      -   5        $0��*Метод простой итерации��5:   '   6G   Q� 5;      -   5        $:��4Нахождение корней полиномов��5;   '   6L   5<      Q� 5=      -   5        $<��6Интерполяция и аппроксимация��5=   #   .@�5>      Q� -   5        $F��@Интерполяционный полином Лагранжа��5>   '   6R   Q� 5?      -	   5        $)��#Кубический сплайны��5?   '   6C   Q� 5@      -
   5        $8��2Метод наименьших квадратов��5@   '   6K   5A      Q� 5B      $5��/Численное интегрирование��6#   .@�5C      Q� -   5        $3��-Формулы прямоугольников��5C   '   6H   Q� 5D      -   5        $%��Формула трапеций��5D   '   6A   Q� 5E      -   5        $%��Формула Симпсона��5E   '   6A   Q� 5F      -   5        $!��Формулы Гаусса��5F   '   6?   5G      5H      5I      5J      �� 5K   
   $ 10q�6   $ 8��6   $��1��6/   $��
10,10,10,0q�6<   $��2��6P   ��5L      �� 5M      $ ScRgbLinearInterpolation=�6&   $ Absolute=�6X   .��5N      � $ Black��6#   $	 0.2Q�61   � 5O      $ 	#FF1B1B1B��6#   $ 1Q�65   5P      ._�5R      �� $��Hidden=�6   .��5S      �� $
��Auto��6   .+�5T      �� $��18(�68   $��Cascadia Code SemiBold)�6G   
���������6    �� *Header+6k   �� 5U      $��16(�6    $��0,20,0,0q�6.   $��Cascadia Code SemiBold)�6A   $��WrapWithOverflow=�6e   
���������6�   �� *Operation.Description+6�   �� 5V      $��16(�6    $��0,20,0,0q�6.   $��Cascadia Code SemiBold)�6A   $��WrapWithOverflow=�6e   
���������6�   �� *Operation.Resolve+6�   5W      5X      5Y      5Z      5[      