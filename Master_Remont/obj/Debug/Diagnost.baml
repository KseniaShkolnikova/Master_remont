   M S B A M L   `   `   `  ����H  DMaster_Remont, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Master_RemontMaster_Remont  5   	       Master_Remont.Diagnost   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Master_Remont     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � Title$  МастерРемонт��5	   	   ���#474747 � WindowState$ 	Maximized=�63    � WindowStartupLocation$ CenterScreen=�6K   $	��850��6p   $
��1500��6}   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      $	��10* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��19* �6   .+�5   
   �� -   5         ��Name$
 exit��5   <   ���#21201F���#21201F$��0��6J   $��50��6Z   $��0��6f   $
��Left=�6�   $��	0,11,0,12q�6�   $��75��6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowLeft=�6&   
�������$��Center=�6J   $��Center=�6g   $��50��6�   $��75��6�   5      � 5   
   $��1��6   $��1��6    5      �� 5      �� 5      $��4* �6#   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��datagrid��5      $��	0,0,20,20q�6   
���   �$��1��6=   ���#B6B6B6   AutoGenerateColumns .     
IsReadOnly . $��18(�6�   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$)
 #Необходимая деталь��6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *SpareParts.SparePartsName+6W     5      $%
 Стоимость детали��6N    6)   �� *SpareParts.Price+6N     5      $
 Статус��6L    6)   �� *Statuses.Names+6L     5       $
 Описание��6J    6)   �� *Descriptionn+6J     5!      $
 Мастер��6M    6)   �� *Employees.Email+6M     5"      $
 Бренд техники��6N    6)   �� *Equipments.Brand+6N     5#      $!
 Модель техники��6N    6)   �� *Equipments.Model+6N     5$      $
 Тип техники��6R    6)   �� *EquipmentTypes.Names+6R   5&      � 5'      $��0��6   $��0,0,10,0q�6#    5(      �� 5)      $��1* �6$   �� 5*      $��1* �6$   �� 5+      $��1* �6$   �� 5,      $��2* �6$   �� 5-      $��5* �6$   .+�5/      �  50      �� 51      $
��1.3* �6+   �� 52      $��1* �6+   .+�54      �� $��20(�6    $��Center=�6.   
�������$ �� Номер заказа: ��6\   �� 55      -   5         ��Name$ nomber��55   �   $��24(�6    $��1��6.   $
��Left=�6>   $��Center=�6Y   
�������6�   56      �� 57       �� CornerRadius$ 15��6   $	��5,0q�6,   $��50��69   ���#B6B6B6���#B6B6B6$	��Top=�6p   $��1��6�   ._�58      �� -   5        $��combobox_status��58   V   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Статус��6   $��20(�6H   $��5q�6m   ���#B6B6B66x   59      �� 5:      $ 15��6   $	��5,0q�6,   $��50��69   ���#B6B6B6���#B6B6B6$	��Top=�6p   $��2��6�   ._�5;      �� -   5        $��combobox_part��5;   V   $  Деталь��6   $��20(�6H   $��5q�6k   ���#B6B6B66v   5<      �� 5=      $��5q�6   $ 15��6$   $��90��66   ���#21201F���#21201F$��1q�6m   $��3��6�   ._�5>      �� -   5        $��description��5>   l   $   Описание��6   $��20(�6J    �� TextWrapping$
 Wrap=�6X   
�������6   5?      �� 5@      -   5        $
��save��5F      $��4��5A      $��70��5B      ���#21201F���#21201F$	��Top=�5E      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��5G       � ToolTip$7 1MaterialDesignRaisedDarkButton with Round Corners��5I      {�5H      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�.��5J      �� $��20(�6    ���#FFC100$��Сохранить��5K      5L      5M      5N      5O      5P      