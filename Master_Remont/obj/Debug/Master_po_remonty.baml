   M S B A M L   `   `   `  ����H  DMaster_Remont, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Master_RemontMaster_Remont  5   	   %    Master_Remont.Master_po_remonty   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Master_Remont     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  МастерРемонт��5	   	   ���#474747 � WindowState$ 	Maximized=�63    � WindowStartupLocation$ CenterScreen=�6K   $	��850��6p   $
��1500��6}   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      $	��13* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $	��18* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$
 exit��5   <   ���#21201F���#21201F$��0��6J   $��70��6Z   $��0��6f   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowLeft=�6&   
�������$��Center=�6J   $��Center=�6g   $��70��6�   $��88��6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��datagrid��5   t   
���   �$��1��6&   $��1��66   ���#B6B6B6   AutoGenerateColumns .     
IsReadOnly . $��18(�6�   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Бренд техники��6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Equipments.Brand+6N     5      $!
 Модель техники��6N    6)   �� *Equipments.Model+6N     5      $
 Статус��6L    6)   �� *Statuses.Names+6L     5      $)
 #Необходимая деталь��6W    6)   �� *SpareParts.SparePartsName+6W     5      $
 Тип техники��6R    6)   �� *EquipmentTypes.Names+6R     5      $%
 Стоимость детали��6N    6)   �� *SpareParts.Price+6N     5       $
 Описание��6J    6)   �� *Descriptionn+6J     5!      $
 Дата приема��6K    6)   �� *ReceptionDate+6K   5#      � 5$   
   $	��5,0q�6   $��1��6   $��2��6)    5%      �� 5&      $��1* �6    �� 5'      $��1* �6    �� 5(      $��6* �6    .+�5*      �  5+      �� 5,      $
��1.3* �6'   �� 5-      $��1* �6'   .+�5/      �� $��24(�6   $��Center=�6*   
�������$ �� Номер заказа: ��6X   �� 50      -   5         ��Name$ nomber��50   �   $��24(�6   $��1��6*   $
��Left=�6:   $��Center=�6U   
�������6�   51      �� 52       �� CornerRadius$ 30��6   $��5,10,0,0q�6(   $��50��6:   ���#B6B6B6���#B6B6B6$	��Top=�6q   $��1��6�   ._�53      �� -   5        $��combobox��53   e   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Статус��6   
���   �$��15(�6W   $��5q�6u   ���#B6B6B66�   54      �� 55      -   5        $��update��5;      $��2��56      $��70��57      ���#21201F���#21201F$	��Top=�5:      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��5<       � ToolTip$7 1MaterialDesignRaisedDarkButton with Round Corners��5>      {�5=      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�.��5?      �� $��20(�6   ���#FFC100$��Сохранить��5@      5A      5B      5C      5D      