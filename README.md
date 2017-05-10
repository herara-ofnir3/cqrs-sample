# CQRS-Sample

ASP.NET MVCでCQRS(コマンドクエリ責務分離)を適用したサンプルです。

このサンプルではCQRSを利用してデータベースアクセスを使い分けています。
コマンド側ではORM(NHibernate)を使い、クエリ側では(Dapperを利用して)SQLを生で扱います。
